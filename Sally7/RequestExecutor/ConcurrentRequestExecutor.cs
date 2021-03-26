using System;
using System.Buffers;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Sally7.Internal;

namespace Sally7.RequestExecutor
{
    /// <summary>
    /// Provides a concurrent request executor that makes use of job ids in the S7 protocol to process responses.
    /// </summary>
    public class ConcurrentRequestExecutor : IRequestExecutor
    {
        private const int JobIdIndex = 12;

        private readonly Socket socket;
        private readonly int bufferSize;
        private readonly int maxRequests;
        private readonly MemoryPool<byte> memoryPool;
        private readonly SocketTpktReader reader;
        private readonly SocketAwaitable sendAwaitable;
        private readonly Request[] requests;
        private readonly Channel<byte> jobChannel;
        private readonly Channel<byte> sendChannel;
        private readonly Channel<byte> receiveChannel;

        /// <inheritdoc/>
        public S7Connection Connection { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentRequestExecutor"/> class with the specified
        /// connection and memory pool.
        /// </summary>
        /// <param name="connection">The <see cref="S7Connection"/> that is used for this executor.</param>
        /// <param name="memoryPool">
        /// The <see cref="MemoryPool{T}" /> that is used for request and response memory allocations.
        /// </param>
        public ConcurrentRequestExecutor(S7Connection connection, MemoryPool<byte> memoryPool)
        {
            if (connection.Parameters == null)
            {
                throw new ArgumentException(
                    "The connection must be initialized and it's Parameters property must have a value.");
            }

            Connection = connection;
            socket = connection.TcpClient.Client;
            bufferSize = connection.Parameters.GetRequiredBufferSize();
            maxRequests = connection.Parameters.MaximumNumberOfConcurrentRequests;
            this.memoryPool = memoryPool;

            var maxNumberOfConcurrentRequests = connection.Parameters.MaximumNumberOfConcurrentRequests;

            jobChannel = Channel.CreateBounded<byte>(maxNumberOfConcurrentRequests);
            sendChannel = Channel.CreateBounded<byte>(1);
            receiveChannel = Channel.CreateBounded<byte>(1);

            if (!Enumerable.Range(1, maxNumberOfConcurrentRequests).All(i => jobChannel.Writer.TryWrite((byte) i)))
                throw new Exception($"Failed to initialize the job channel.");

            if (!sendChannel.Writer.TryWrite(1)) throw new Exception("Failed to initialize the sending channel.");
            if (!receiveChannel.Writer.TryWrite(1)) throw new Exception("Failed to initialize the receiving channel.");

            requests = Enumerable.Range(0, maxNumberOfConcurrentRequests).Select(_ => new Request()).ToArray();

            reader = new SocketTpktReader(socket);
            sendAwaitable = new SocketAwaitable(new SocketAsyncEventArgs());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentRequestExecutor"/> class with the specified
        /// connection.
        /// </summary>
        /// <param name="connection">The <see cref="S7Connection"/> that is used for this executor.</param>
        public ConcurrentRequestExecutor(S7Connection connection) : this(connection, MemoryPool<byte>.Shared)
        {
        }

        /// <inheritdoc/>
        public async ValueTask<Memory<byte>> PerformRequest(ReadOnlyMemory<byte> request, Memory<byte> response)
        {
            var id = await jobChannel.Reader.ReadAsync().ConfigureAwait(false);
            try
            {
                var req = requests[id - 1];
                req.Reset();
                req.SetBuffer(response);

                using (var mo = memoryPool.Rent(request.Length))
                {
                    request.CopyTo(mo.Memory);
                    mo.Memory.Span[JobIdIndex] = id;

                    if (!MemoryMarshal.TryGetArray(mo.Memory.Slice(0, request.Length), out ArraySegment<byte> segment))
                    {
                        throw new Exception("Failed to get array from Memory");
                    }

                    await sendChannel.Reader.ReadAsync().ConfigureAwait(false);
                    try
                    {
                        sendAwaitable.EventArgs.SetBuffer(segment.Array, segment.Offset, segment.Count);
                        await socket.SendAsync(sendAwaitable);
                    }
                    finally
                    {
                        if (!sendChannel.Writer.TryWrite(id))
                        {
                            throw new Exception("Couldn't signal send channel.");
                        }
                    }
                }

                // Always wait for a response. The number of received responses should always equal the
                // number of requests, so a single response must be received.
                Request rec;
                int length;

                using (var mo = memoryPool.Rent(bufferSize))
                {
                    await receiveChannel.Reader.ReadAsync().ConfigureAwait(false);
                    try
                    {
                        length = await reader.ReadAsync(mo.Memory).ConfigureAwait(false);
                    }
                    finally
                    {
                        if (!receiveChannel.Writer.TryWrite(0))
                        {
                            throw new Exception("Couldn't signal receive channel.");
                        }
                    }

                    var message = mo.Memory.Slice(0, length);
                    var replyJobId = mo.Memory.Span[JobIdIndex];

                    if (replyJobId <= 0 || replyJobId > maxRequests)
                    {
                        throw new S7CommunicationException($"Received invalid job ID '{replyJobId}' in response from PLC.",
                            message.ToArray());
                    }

                    rec = requests[replyJobId - 1];

                    mo.Memory.Slice(0, length).CopyTo(rec.Buffer);
                }

                rec.Complete(length);

                // await the actual completion before returning this job ID to the pool
                return await req;
            }
            finally
            {
                if (!jobChannel.Writer.TryWrite(id))
                {
                    throw new Exception($"Couldn't return job ID {id} to the pool.");
                }
            }
        }

        private class Request : INotifyCompletion
        {
            private static readonly Action Sentinel = () => { };

            private Memory<byte> buffer;

            public bool IsCompleted { get; private set; }
            private int length;
            private Action continuation = Sentinel;

            public Memory<byte> Buffer => buffer;

            public void Complete(int length)
            {
                this.length = length;

                var prev = continuation ?? Interlocked.CompareExchange(ref continuation, Sentinel, null);
                prev?.Invoke();
            }

            public Memory<byte> GetResult()
            {
                return buffer.Slice(0, length);
            }

            public Request GetAwaiter() => this;

            public void OnCompleted(Action continuation)
            {
                if (this.continuation == Sentinel ||
                    Interlocked.CompareExchange(ref this.continuation, continuation, null) == Sentinel)
                {
                    continuation.Invoke();
                }
            }

            public void Reset()
            {
                continuation = null;
                IsCompleted = false;
            }

            public void SetBuffer(Memory<byte> buffer)
            {
                this.buffer = buffer;
            }
        }
    }
}
