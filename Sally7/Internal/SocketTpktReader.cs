using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sally7.Protocol.IsoOverTcp;

namespace Sally7.Internal
{
    internal class SocketTpktReader
    {
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
                throw new Exception($"Memory was not array based");

            args.SetBuffer(segment.Array, segment.Offset, Tpkt.Size);

            var count = 0;
            do
            {
                if (count > 0)
                {
                    args.SetBuffer(segment.Offset + count, Tpkt.Size - count);
                }

                await socket.ReceiveAsync(awaitable);

                if (args.BytesTransferred <= 0)
                    throw new Exception("Connection was closed while reading.");

                count += args.BytesTransferred;

            } while (count < Tpkt.Size);

            var receivedLength = GetTpktLength(message.Span);

            while (count < receivedLength)
            {
                args.SetBuffer(segment.Offset + count, receivedLength - count);
                await socket.ReceiveAsync(awaitable);

                if (args.BytesTransferred <= 0)
                    throw new Exception("Connection was closed while reading.");

                count += args.BytesTransferred;
            }

            return receivedLength;
        }

        private static int GetTpktLength(in ReadOnlySpan<byte> span)
        {
            try
            {
                ref readonly var tpkt = ref MemoryMarshal.Cast<byte, Tpkt>(span)[0];
                tpkt.Assert();

                return tpkt.Length;
            }
            catch (Exception e)
            {
                var data = span.ToArray();

                throw new S7CommunicationException(
                    $"Failed to parse TPKT from response ({string.Join(", ", data.Select(b => b.ToString("X2")))}), " +
                    $"see the {nameof(S7CommunicationException.InnerException)} property for details.", e,
                    span.ToArray());
            }
        }
    }
}
