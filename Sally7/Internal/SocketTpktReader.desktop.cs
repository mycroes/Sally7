#if !NETSTANDARD2_1_OR_GREATER && !NET5_0_OR_GREATER

using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Sally7.Internal
{
    internal partial class SocketTpktReader
    {
        private readonly SocketAwaitable _awaitable;
        private readonly SocketAsyncEventArgs _args;

        public SocketTpktReader(Socket socket)
        {
            _socket = socket;
            _args = new SocketAsyncEventArgs();
            _awaitable = new SocketAwaitable(_args);
        }

        public async ValueTask<int> ReadAsync(Memory<byte> message, CancellationToken cancellationToken)
        {
            if (!MemoryMarshal.TryGetArray<byte>(message, out var segment))
            {
                Sally7Exception.ThrowMemoryWasNotArrayBased();
            }

            _args.SetBuffer(segment.Array, segment.Offset, TpktSize);

            int count = 0;
            do
            {
                if (count > 0)
                {
                    _args.SetBuffer(segment.Offset + count, TpktSize - count);
                }

                cancellationToken.ThrowIfCancellationRequested();
                await _socket.ReceiveAsync(_awaitable);

                if (_args.BytesTransferred <= 0)
                {
                    TpktException.ThrowConnectionWasClosedWhileReading();
                }

                count += _args.BytesTransferred;

            } while (count < TpktSize);

            int receivedLength = GetTpktLength(message.Span);

            while (count < receivedLength)
            {
                cancellationToken.ThrowIfCancellationRequested();
                _args.SetBuffer(segment.Offset + count, receivedLength - count);
                await _socket.ReceiveAsync(_awaitable);

                if (_args.BytesTransferred <= 0)
                {
                    TpktException.ThrowConnectionWasClosedWhileReading();
                }

                count += _args.BytesTransferred;
            }

            return receivedLength;
        }
    }
}

#endif
