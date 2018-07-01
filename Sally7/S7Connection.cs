using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Sally7.Infrastructure;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.IsoOverTcp;

namespace Sally7
{
    public class S7Connection
    {
        private const int IsoOverTcpPort = 102;

        private readonly string host;
        private readonly Tsap sourceTsap;
        private readonly Tsap destinationTsap;
        private readonly TcpClient client = new TcpClient {NoDelay = true};

        private byte[] buffer = new byte[100];
        private int pduSize;

        public S7Connection(in string host, in Tsap sourceTsap, in Tsap destinationTsap)
        {
            this.host = host;
            this.sourceTsap = sourceTsap;
            this.destinationTsap = destinationTsap;
        }

        public void Close()
        {
            client.Close();
        }

        public async Task Open()
        {
            await client.ConnectAsync(host, IsoOverTcpPort).ConfigureAwait(false);
            var stream = client.GetStream();
            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildConnectRequest(buffer, sourceTsap, destinationTsap)).ConfigureAwait(false);
            var length = await ReadTpkt().ConfigureAwait(false);
            S7ConnectionHelpers.ParseConnectionConfirm(buffer.AsSpan().Slice(0, length));

            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildCommunicationSetup(buffer)).ConfigureAwait(false);
            length = await ReadTpkt().ConfigureAwait(false);
            S7ConnectionHelpers.ParseCommunicationSetup(buffer.AsSpan().Slice(0, length), out pduSize);

            // TPKT + COTP DT + S7 PDU, assumes TPKT + COTP DT don't count as PDU data
            buffer = new byte[pduSize + 7];
        }

        public async Task Read(params IDataItem[] dataItems)
        {
            var stream = client.GetStream();
            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildReadRequest(buffer, dataItems)).ConfigureAwait(false);
            var length = await ReadTpkt().ConfigureAwait(false);
            S7ConnectionHelpers.ParseReadResponse(buffer.AsSpan().Slice(0, length), dataItems);
        }

        public async Task Write(params IDataItem[] dataItems)
        {
            var stream = client.GetStream();
            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildWriteRequest(buffer, dataItems)).ConfigureAwait(false);
            var length = await ReadTpkt().ConfigureAwait(false);
            S7ConnectionHelpers.ParseWriteResponse(buffer.AsSpan().Slice(0, length), dataItems);
        }

        private async Task<int> ReadTpkt()
        {
            var stream = client.GetStream();

            var len = await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false);
            if (len < 4)
                throw new Exception($"Error while reading TPKT header, expected 4 bytes but received {len}.");

            var tpkt = buffer.AsSpan().Struct<Tpkt>(0);
            tpkt.Assert();
            var msgLen = tpkt.MessageLength();
            len = await stream.ReadAsync(buffer, 4, msgLen);
            if (len != msgLen)
            {
                throw new Exception($"Error while reading TPKT data, expected {msgLen} bytes but received {len}.");
            }

            return tpkt.Length;
        }
    }
}
