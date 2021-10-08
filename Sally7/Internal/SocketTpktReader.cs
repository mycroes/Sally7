using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sally7.Infrastructure;
using Sally7.Protocol.IsoOverTcp;

namespace Sally7.Internal
{
    internal class SocketTpktReader
    {
        private const int TpktSize = 4;

        private readonly Socket socket;
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

            var count = 0;
            do
            {
                if (count > 0)
                {
                    args.SetBuffer(segment.Offset + count, TpktSize - count);
                }

                await socket.ReceiveAsync(awaitable);

                if (args.BytesTransferred <= 0)
                    TpktException.ThrowConnectionWasClosedWhileReading();

                count += args.BytesTransferred;

            } while (count < TpktSize);

            var receivedLength = GetTpktLength(message.Span);

            while (count < receivedLength)
            {
                args.SetBuffer(segment.Offset + count, receivedLength - count);
                await socket.ReceiveAsync(awaitable);

                if (args.BytesTransferred <= 0)
                    TpktException.ThrowConnectionWasClosedWhileReading();

                count += args.BytesTransferred;
            }

            return receivedLength;
        }

        private static int GetTpktLength(ReadOnlySpan<byte> span)
        {
            try
            {
                ref readonly var tpkt = ref span.Struct<Tpkt>(0);
                tpkt.Assert();

                return tpkt.Length;
            }
            catch (Exception e)
            {
                S7CommunicationException.ThrowFailedToParseResponse(span, e);
                return -1;  // to make the compiler happy
            }
        }
    }
}
