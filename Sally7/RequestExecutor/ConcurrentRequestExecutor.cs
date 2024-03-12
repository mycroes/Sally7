using System;
using System.Buffers;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
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

                    // If we bail while sending the PLC might still respond to the data that was sent.
                    // This both breaks the send-one-receive-one flow as well as it might end up
                    // completing a new job that reused the ID.
                    var closeOnCancel = cancellationToken.MaybeUnsafeRegister(SocketHelper.CloseSocketCallback, socket);
                    try
                    {
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
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                        await closeOnCancel.DisposeAsync().ConfigureAwait(false);
#else
                        closeOnCancel.Dispose();
#endif

                        if (!sendSignal.TryRelease())
                        {
                            Sally7Exception.ThrowFailedToSignalSendDone();
                        }
                    }
                }

                // Always wait for a response. The number of received responses should always equal the
                // number of requests, so a single response must be received.
                Request rec;
                var length = 0;

                using (IMemoryOwner<byte> mo = memoryPool.Rent(bufferSize))
                {
                    _ = await receiveSignal.WaitAsync(cancellationToken).ConfigureAwait(false);

                    // If we bail while reading we break the send-one-receive-one flow, so we might as well close right away.
                    // There is minimal risk of closing connections while data was actually received but handling here
                    // avoids registering on the cancellationToken on every socket call.
                    var closeOnCancel =
                        cancellationToken.MaybeUnsafeRegister(SocketHelper.CloseSocketCallback, socket);
                    try
                    {
                        length = await reader.ReadAsync(mo.Memory, cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception) when (cancellationToken.IsCancellationRequested)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    finally
                    {
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                        await closeOnCancel.DisposeAsync().ConfigureAwait(false);
#else
                        closeOnCancel.Dispose();
#endif

                        if (!receiveSignal.TryRelease())
                        {
                            Sally7Exception.ThrowFailedToSignalReceiveDone();
                        }
                    }

                    Memory<byte> message = mo.Memory.Slice(0, length);
                    int replyJobId = mo.Memory.Span[JobIdIndex];

                    if ((uint)(replyJobId - 1) >= (uint)maxRequests)
                    {
                        // todo: This is breaking, because we return the current jobId to the pool
                        // so when that gets answered it might complete an incorrect request.
                        S7CommunicationException.ThrowInvalidJobID(replyJobId, message);
                    }

                    // todo: There's no state validation on the request, potentially this is not rented out at all.
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
    }
}
