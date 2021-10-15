using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Sally7.Internal
{
    internal partial class SocketTpktReader
    {
        private readonly SocketAwaitable awaitable;
        private readonly SocketAsyncEventArgs args;

        public SocketTpktReader(Socket socket)
        {
            this.socket = socket;
            args = new SocketAsyncEventArgs();
            awaitable = new SocketAwaitable(args);
        }

        public async ValueTask<int> ReadAsync(Memory<byte> message)
        {
            if (!MemoryMarshal.TryGetArray<byte>(message, out var segment))
            {
                Sally7Exception.ThrowMemoryWasNotArrayBased();
            }

            args.SetBuffer(segment.Array, segment.Offset, TpktSize);

            int count = 0;
            do
            {
                if (count > 0)
                {
                    args.SetBuffer(segment.Offset + count, TpktSize - count);
                }

                await socket.ReceiveAsync(awaitable);

                if (args.BytesTransferred <= 0)
                {
                    TpktException.ThrowConnectionWasClosedWhileReading();
                }

                count += args.BytesTransferred;

            } while (count < TpktSize);

            int receivedLength = GetTpktLength(message.Span);

            while (count < receivedLength)
            {
                args.SetBuffer(segment.Offset + count, receivedLength - count);
                await socket.ReceiveAsync(awaitable);

                if (args.BytesTransferred <= 0)
                {
                    TpktException.ThrowConnectionWasClosedWhileReading();
                }

                count += args.BytesTransferred;
            }

            return receivedLength;
        }
    }
}
