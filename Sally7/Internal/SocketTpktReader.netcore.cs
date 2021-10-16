#if NETSTANDARD2_1 || NET5_0_OR_GREATER

using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sally7.Internal
{
    internal partial class SocketTpktReader
    {
        public SocketTpktReader(Socket socket) => this.socket = socket;

        public async ValueTask<int> ReadAsync(Memory<byte> message)
        {
            int count = 0;
            Memory<byte> buffer = message.Slice(0, TpktSize);
            do
            {
                if (count > 0)
                {
                    buffer = message[count..TpktSize];
                }

                int read = await socket.ReceiveAsync(buffer, SocketFlags.None).ConfigureAwait(false);

                if (read <= 0)
                {
                    TpktException.ThrowConnectionWasClosedWhileReading();
                }

                count += read;

            } while (count < TpktSize);

            int receivedLength = GetTpktLength(message.Span);

            while (count < receivedLength)
            {
                buffer = message[count..receivedLength];
                int read = await socket.ReceiveAsync(buffer, SocketFlags.None).ConfigureAwait(false);

                if (read <= 0)
                {
                    TpktException.ThrowConnectionWasClosedWhileReading();
                }

                count += read;
            }

            return receivedLength;
        }
    }
}

#endif
