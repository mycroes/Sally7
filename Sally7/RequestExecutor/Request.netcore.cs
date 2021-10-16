#if NETSTANDARD2_1 || NET5_0_OR_GREATER

using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Sally7.RequestExecutor
{
    internal sealed class Request : IValueTaskSource<Memory<byte>>
    {
        private ManualResetValueTaskSourceCore<Memory<byte>> mrvts = new(); // mutable struct, don't make readonly!
        private Memory<byte> buffer;

        public Memory<byte> Buffer => buffer;

        public void Reset() => mrvts.Reset();

        public void SetBuffer(Memory<byte> buffer) => this.buffer = buffer;

        public ValueTask<Memory<byte>> GetValueTask() => new ValueTask<Memory<byte>>(this, mrvts.Version);

        public void Complete(int length) => mrvts.SetResult(buffer.Slice(0, length));

        public Memory<byte> GetResult(short token) => mrvts.GetResult(token);
        public ValueTaskSourceStatus GetStatus(short token) => mrvts.GetStatus(token);
        public void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
            => mrvts.OnCompleted(continuation, state, token, flags);
    }
}

#endif
