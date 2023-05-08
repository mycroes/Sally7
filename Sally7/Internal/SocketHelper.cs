using System;
using System.Net.Sockets;

namespace Sally7.Internal
{
    internal static class SocketHelper
    {
        public static void CloseSocketCallback(object? state)
        {
            var socket = state as Socket ?? throw new ArgumentException(
                $"State passed to {nameof(CloseSocketCallback)} must be a {nameof(Socket)}, received '{state}' instead.",
                nameof(state));

            socket.Close();
        }
    }
}