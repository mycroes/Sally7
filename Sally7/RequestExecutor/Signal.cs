using System;
using System.Diagnostics;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sally7.RequestExecutor
{
    [DebuggerNonUserCode]
    [DebuggerDisplay(nameof(NeedToWait) + ": {" + nameof(NeedToWait) + ",nq}")]
    internal sealed class Signal : IDisposable
    {
        private readonly Channel<int> channel = Channel.CreateBounded<int>(1);

        public void Dispose() => channel.Writer.Complete();

        public bool TryInit() => channel.Writer.TryWrite(0);

        public ValueTask<int> WaitAsync() => channel.Reader.ReadAsync();

        public bool TryRelease() => channel.Writer.TryWrite(0);

        private bool NeedToWait => channel.Reader.Count == 0;
    }
}
