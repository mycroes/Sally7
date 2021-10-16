#if !NETSTANDARD2_1_OR_GREATER && !NET5_0_OR_GREATER

using System.Net.Sockets;

namespace Sally7.Internal
{
    /// <summary>
    /// Socket extensions to send and receive using <see cref="SocketAwaitable" />.
    /// </summary>
    /// <remarks>
    /// Based on https://devblogs.microsoft.com/pfxteam/awaiting-socket-operations/.
    /// </remarks>
    internal static class SocketExtensions
    {
        public static SocketAwaitable ReceiveAsync(this Socket socket, SocketAwaitable awaitable)
        {
            awaitable.Reset();
            if (!socket.ReceiveAsync(awaitable.EventArgs))
                awaitable.WasCompleted = true;
            return awaitable;
        }

        public static SocketAwaitable SendAsync(this Socket socket, SocketAwaitable awaitable)
        {
            awaitable.Reset();
            if (!socket.SendAsync(awaitable.EventArgs))
                awaitable.WasCompleted = true;
            return awaitable;
        }
    }
}

#endif
