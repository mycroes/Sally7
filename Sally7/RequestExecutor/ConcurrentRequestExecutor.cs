using System;
using System.Buffers;
using System.Diagnostics;
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
    internal sealed class ConcurrentRequestExecutor : IRequestExecutor
    {
        private const int JobIdIndex = 12;

        private readonly Socket socket;
        private readonly int bufferSize;
        private readonly int maxRequests;
        private readonly MemoryPool<byte> memoryPool;
        private readonly SocketTpktReader reader;
        private readonly JobPool jobPool;
        private readonly Signal sendSignal;
        private readonly Signal receiveSignal;

#if !NETSTANDARD2_1_OR_GREATER && !NET5_0_OR_GREATER
        private readonly SocketAwaitable sendAwaitable;
#endif

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
        public ConcurrentRequestExecutor(S7Connection connection, MemoryPool<byte>? memoryPool = default)
        {
            if (connection.Parameters == null)
            {
                Sally7CommunicationSetupException.ThrowConnectionParametersNotSet();
            }

            Connection = connection;
            socket = connection.TcpClient.Client;
            bufferSize = connection.Parameters.GetRequiredBufferSize();
            maxRequests = connection.Parameters.MaximumNumberOfConcurrentRequests;
            this.memoryPool = memoryPool ?? MemoryPool<byte>.Shared;

            jobPool = new JobPool(connection.Parameters.MaximumNumberOfConcurrentRequests);
            sendSignal = new Signal();
            receiveSignal = new Signal();

            if (!sendSignal.TryInit()) Sally7Exception.ThrowFailedToInitSendingSignal();
            if (!receiveSignal.TryInit()) Sally7Exception.ThrowFailedToInitReceivingSignal();

            reader = new SocketTpktReader(socket);

#if !NETSTANDARD2_1_OR_GREATER && !NET5_0_OR_GREATER
            sendAwaitable = new SocketAwaitable(new SocketAsyncEventArgs());
#endif
        }

        public void Dispose()
        {
            jobPool.Dispose();
            sendSignal.Dispose();
            receiveSignal.Dispose();
        }

        /// <inheritdoc/>
        public async ValueTask<Memory<byte>> PerformRequest(ReadOnlyMemory<byte> request, Memory<byte> response, CancellationToken cancellationToken)
        {
            int jobId = await jobPool.RentJobIdAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                jobPool.SetBufferForRequest(jobId, response);

                using (IMemoryOwner<byte> mo = memoryPool.Rent(request.Length))
                {
                    request.CopyTo(mo.Memory);
                    mo.Memory.Span[JobIdIndex] = (byte) jobId;

                    _ = await sendSignal.WaitAsync(cancellationToken).ConfigureAwait(false);
                    try
                    {
                        // If we bail while sending the PLC might still respond to the data that was sent.
                        // This both breaks the send-one-receive-one flow as well as it might end up
                        // completing a new job that reused the ID.
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                        await
#endif
                        using var closeOnCancel = cancellationToken.MaybeUnsafeRegister(socket.Close);

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
                        int written = await socket.SendAsync(mo.Memory.Slice(0, request.Length), SocketFlags.None, cancellationToken).ConfigureAwait(false);
                        Debug.Assert(written == request.Length);
#else
                        if (!MemoryMarshal.TryGetArray(mo.Memory.Slice(0, request.Length), out ArraySegment<byte> segment))
                        {
                            Sally7Exception.ThrowMemoryWasNotArrayBased();
                        }

                        sendAwaitable.EventArgs.SetBuffer(segment.Array, segment.Offset, segment.Count);
                        await socket.SendAsync(sendAwaitable);
#endif
                    }
                    finally
                    {
                        if (!sendSignal.TryRelease())
                        {
                            Sally7Exception.ThrowFailedToSignalSendDone();
                        }
                    }
                }

                // Always wait for a response. The number of received responses should always equal the
                // number of requests, so a single response must be received.
                Request rec;
                int length;

                using (IMemoryOwner<byte> mo = memoryPool.Rent(bufferSize))
                {
                    _ = await receiveSignal.WaitAsync(cancellationToken).ConfigureAwait(false);
                    try
                    {
                        // If we bail while reading we break the send-one-receive-one flow, so we might as well close right away.
                        // There is minimal risk of closing connections while data was actually received but handling here
                        // avoids registering on the cancellationToken on every socket call.
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                        await
#endif
                        using var closeOnCancel = cancellationToken.MaybeUnsafeRegister(socket.Close);

                        try
                        {
                            length = await reader.ReadAsync(mo.Memory, cancellationToken).ConfigureAwait(false);
                        }
                        catch (Exception) when (cancellationToken.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            // Should never happen, but required to satisfy compiler analysis of length initialization.
                            throw new OperationCanceledException(cancellationToken);
                        }
                    }
                    finally
                    {
                        if (!receiveSignal.TryRelease())
                        {
                            Sally7Exception.ThrowFailedToSignalReceiveDone();
                        }
                    }

                    Memory<byte> message = mo.Memory.Slice(0, length);
                    int replyJobId = mo.Memory.Span[JobIdIndex];

                    if ((uint)(replyJobId - 1) >= (uint)maxRequests)
                    {
                        S7CommunicationException.ThrowInvalidJobID(replyJobId, message);
                    }

                    rec = jobPool.GetRequest(replyJobId);

                    message.CopyTo(rec.Buffer);
                }

                rec.Complete(length);

                // await the actual completion before returning this job ID to the pool
                return await jobPool.GetRequest(jobId);
            }
            finally
            {
                jobPool.ReturnJobId(jobId);
            }
        }

        private class JobPool : IDisposable
        {
            private readonly Channel<int> jobIdPool;
            private readonly Request[] requests;

            public JobPool(int maxNumberOfConcurrentRequests)
            {
                jobIdPool = Channel.CreateBounded<int>(maxNumberOfConcurrentRequests);
                requests = new Request[maxNumberOfConcurrentRequests];

                for (int i = 0; i < maxNumberOfConcurrentRequests; ++i)
                {
                    if (!jobIdPool.Writer.TryWrite(i + 1))
                    {
                        Sally7Exception.ThrowFailedToInitJobPool();
                    }

                    requests[i] = new Request();
                }
            }

            public void Dispose() => jobIdPool.Writer.Complete();

            public ValueTask<int> RentJobIdAsync(CancellationToken cancellationToken) => jobIdPool.Reader.ReadAsync(cancellationToken);

            public void ReturnJobId(int jobId)
            {
                if (!jobIdPool.Writer.TryWrite(jobId))
                {
                    Sally7Exception.ThrowFailedToReturnJobIDToPool(jobId);
                }
            }

            [DebuggerNonUserCode]
            public Request GetRequest(int jobId) => requests[jobId - 1];

            public void SetBufferForRequest(int jobId, Memory<byte> buffer)
            {
                Request req = GetRequest(jobId);
                req.Reset();
                req.SetBuffer(buffer);
            }
        }

        [DebuggerNonUserCode]
        [DebuggerDisplay(nameof(NeedToWait) + ": {" + nameof(NeedToWait) + ",nq}")]
        private class Signal : IDisposable
        {
            private readonly Channel<int> channel = Channel.CreateBounded<int>(1);

            public void Dispose() => channel.Writer.Complete();

            public bool TryInit() => channel.Writer.TryWrite(0);

            public ValueTask<int> WaitAsync(CancellationToken cancellationToken) => channel.Reader.ReadAsync(cancellationToken);

            public bool TryRelease() => channel.Writer.TryWrite(0);

            private bool NeedToWait => channel.Reader.Count == 0;
        }

        private class Request : INotifyCompletion
        {
            private static readonly Action Sentinel = () => { };

            private Memory<byte> buffer;

            public bool IsCompleted { get; private set; }
            private int length;
            private Action? continuation = Sentinel;

            public Memory<byte> Buffer => buffer;

            public void Complete(int length)
            {
                this.length = length;
                IsCompleted = true;

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
