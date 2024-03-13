#if !NETSTANDARD2_1_OR_GREATER && !NET5_0_OR_GREATER

using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sally7.Internal
{
    /// <summary>
    /// Reusable awaitable for socket operations.
    /// </summary>
    /// <remarks>
    /// Based on https://devblogs.microsoft.com/pfxteam/awaiting-socket-operations/.
    /// </remarks>
    internal sealed class SocketAwaitable : INotifyCompletion
    {
        private static readonly Action Sentinel = () => { };

        public bool WasCompleted;
        private Action? _continuation;
        public readonly SocketAsyncEventArgs EventArgs;

        public SocketAwaitable(SocketAsyncEventArgs eventArgs)
        {
            EventArgs = eventArgs ?? throw new ArgumentNullException(nameof(eventArgs));
            eventArgs.Completed += delegate
            {
                var prev = _continuation ?? Interlocked.CompareExchange(ref _continuation, Sentinel, null);
                prev?.Invoke();
            };
        }

        internal void Reset()
        {
            WasCompleted = false;
            _continuation = null;
        }

        public SocketAwaitable GetAwaiter()
        {
            return this;
        }

        public bool IsCompleted => WasCompleted;

        public void OnCompleted(Action continuation)
        {
            if (this._continuation == Sentinel ||
                Interlocked.CompareExchange(ref this._continuation, continuation, null) == Sentinel)
            {
                continuation.Invoke();
            }
        }

        public void GetResult()
        {
            if (EventArgs.SocketError != SocketError.Success)
            {
                Sally7Exception.ThrowSocketException(EventArgs.SocketError);
            }
        }
    }
}

#endif
