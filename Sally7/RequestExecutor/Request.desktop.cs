#if !NETSTANDARD2_1_OR_GREATER && !NET5_0_OR_GREATER

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sally7.RequestExecutor
{
    internal sealed class Request : INotifyCompletion
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

        public Memory<byte> GetResult() => buffer.Slice(0, length);

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

        public void SetBuffer(Memory<byte> buffer) => this.buffer = buffer;
    }
}

#endif
