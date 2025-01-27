using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Sally7.Internal;

namespace Sally7.RequestExecutor;

internal class JobPool : IDisposable
{
    private readonly Channel<int> _jobIdPool;
    private readonly Request[] _requests;
    private bool _disposed;

    public JobPool(int maxNumberOfConcurrentRequests)
    {
        _jobIdPool = Channel.CreateBounded<int>(maxNumberOfConcurrentRequests);
        _requests = new Request[maxNumberOfConcurrentRequests];

        for (int i = 0; i < maxNumberOfConcurrentRequests; ++i)
        {
            if (!_jobIdPool.Writer.TryWrite(i + 1))
            {
                Sally7Exception.ThrowFailedToInitJobPool();
            }

            _requests[i] = new Request();
        }
    }

    public void Dispose()
    {
        Volatile.Write(ref _disposed, true);
        _jobIdPool.Writer.Complete();

        foreach (var request in _requests)
        {
            request.Dispose();
        }
    }

    public ValueTask<int> RentJobIdAsync(CancellationToken cancellationToken) => _jobIdPool.Reader.ReadAsync(cancellationToken);

    public void ReturnJobId(int jobId)
    {
        if (!_jobIdPool.Writer.TryWrite(jobId) && !Volatile.Read(ref _disposed))
        {
            Sally7Exception.ThrowFailedToReturnJobIDToPool(jobId);
        }
    }

    [DebuggerNonUserCode]
    public Request GetRequest(int jobId)
    {
        DisposableHelper.ThrowIf(Volatile.Read(ref _disposed), this);

        return _requests[jobId - 1];
    }

    public void SetBufferForRequest(int jobId, Memory<byte> buffer)
    {
        Request req = GetRequest(jobId);
        req.Reset();
        req.SetBuffer(buffer);
    }
}