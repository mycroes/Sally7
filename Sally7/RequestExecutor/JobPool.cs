using System;
using System.Diagnostics;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sally7.RequestExecutor
{
    internal sealed class JobPool : IDisposable
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

        public ValueTask<int> RentJobIdAsync() => jobIdPool.Reader.ReadAsync();

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
}
