using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sally7.RequestExecutor;

internal class Request : INotifyCompletion
{
    private static readonly Action Sentinel = () => { };

    private Memory<byte> _buffer;

    public bool IsCompleted { get; private set; }
    private int _length;
    private Action? _continuation = Sentinel;

    public Memory<byte> Buffer => _buffer;

    public void Complete(int length)
    {
        this._length = length;
        IsCompleted = true;

        var prev = _continuation ?? Interlocked.CompareExchange(ref _continuation, Sentinel, null);
        prev?.Invoke();
    }

    public Memory<byte> GetResult()
    {
        return _buffer.Slice(0, _length);
    }

    public Request GetAwaiter() => this;

    public void OnCompleted(Action continuation)
    {
        if (this._continuation == Sentinel ||
            Interlocked.CompareExchange(ref this._continuation, continuation, null) == Sentinel)
        {
            continuation.Invoke();
        }
    }

    public void Reset()
    {
        _continuation = null;
        IsCompleted = false;
    }

    public void SetBuffer(Memory<byte> buffer)
    {
        this._buffer = buffer;
    }
}