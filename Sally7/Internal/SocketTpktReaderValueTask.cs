#if NETSTANDARD2_1 || NET5_0_OR_GREATER

using System;
using System.Net.Sockets;
using Sally7.Infrastructure;
using Sally7.Protocol.IsoOverTcp;

namespace Sally7.Internal
{
    internal partial class SocketTpktReaderValueTask
    {
        private const int TpktSize = 4;

        private readonly Socket socket;

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

#endif
