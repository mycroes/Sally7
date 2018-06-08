using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sally7.Infrastructure;
using Sally7.Protocol;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.Cotp.Messages;
using Sally7.Protocol.IsoOverTcp;
using Sally7.Protocol.S7;
using Sally7.Protocol.S7.Messages;

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

        public S7Connection(string host, Tsap sourceTsap, Tsap destinationTsap)
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
            await stream.WriteAsync(buffer, 0, BuildConnectRequest()).ConfigureAwait(false);
            ParseConnectionConfirm(await ReadTpkt().ConfigureAwait(false));

            await stream.WriteAsync(buffer, 0, BuildCommunicationSetup()).ConfigureAwait(false);
            ParseCommunicationSetup(await ReadTpkt().ConfigureAwait(false));
        }

        private async Task<int> ReadTpkt()
        {
            var stream = client.GetStream();

            var len = await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false);
            if (len < 4)
                throw new Exception($"Error while reading TPKT header, expected 4 bytes but received {len}.");

            buffer.Struct<Tpkt>(0).Assert();
            var msgLen = buffer.Struct<Tpkt>(0).MessageLength();
            len = await stream.ReadAsync(buffer, 4, msgLen);
            if (len != msgLen)
            {
                throw new Exception($"Error while reading TPKT data, expected {msgLen} bytes but received {len}.");
            }

            return buffer.Struct<Tpkt>(0).Length;
        }

        private int BuildConnectRequest()
        {
            ref var message = ref buffer.Struct<ConnectionRequestMessage>(4);
            message.Init(PduSizeParameter.PduSize.Pdu512, sourceTsap, destinationTsap);
            var len = 4 + ConnectionRequestMessage.Size;
            buffer.Struct<Tpkt>(0).Init(len);

            DumpBuffer(len);

            return len;
        }

        private int BuildCommunicationSetup()
        {
            ref var data = ref buffer.Struct<Data>(4);
            data.Init();

            ref var header = ref buffer.Struct<Header>(7);
            header.Init(MessageType.JobRequest, CommunicationSetup.Size, 0);

            // Error class and error code are not used, so next starts at 7 + 10
            ref var setup = ref buffer.Struct<CommunicationSetup>(17);
            setup.Init(1, 1, 1920);

            var len = 17 + CommunicationSetup.Size;
            buffer.Struct<Tpkt>(0).Init(len);

            DumpBuffer(len);

            return len;
        }

        private void ParseConnectionConfirm(in int length)
        {
            DumpBuffer(length);

            var fixedPartLength = buffer[5];
            if (fixedPartLength < ConnectionConfirm.Size)
                throw new Exception("Received data is smaller than Connection Confirm fixed part.");

            ref var cc = ref buffer.Struct<ConnectionConfirm>(5);
            cc.Assert();

            // Analyze returned parameters?
        }

        private void ParseCommunicationSetup(in int length)
        {
            DumpBuffer(length);
            if (length < 19 + CommunicationSetup.Size) throw new Exception("Received data is smaller than TPKT + DT PDU + S7 header + S7 communication setup size.");
            ref var dt = ref buffer.Struct<Data>(4);
            dt.Assert();

            ref var s7Header = ref buffer.Struct<Header>(7);
            s7Header.Assert(MessageType.AckData);

            ref var s7CommunicationSetup = ref buffer.Struct<CommunicationSetup>(19);
            s7CommunicationSetup.Assert(FunctionCode.CommunicationSetup);

            pduSize = s7CommunicationSetup.PduSize;
            // TPKT + COTP DT + S7 PDU, assumes TPKT + COTP DT don't count as PDU data
            buffer = new byte[pduSize + 7];
        }

        private void DumpBuffer(in int length, [CallerMemberName] string caller = null)
        {
            Console.WriteLine($"{caller}: {string.Join(", ", buffer.Take(length).Select(b => $"{b:X}"))}");
            Console.WriteLine($"{caller}: {string.Join(", ", buffer.Take(length).Select(b => b))}");
        }
    }
}
