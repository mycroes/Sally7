using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sally7.Infrastructure;
using Sally7.Protocol;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.Cotp.Messages;
using Sally7.Protocol.IsoOverTcp;
using Sally7.Protocol.S7;
using Sally7.Protocol.S7.Messages;
using Sally7.ValueConversion;

namespace Sally7
{
    public class S7Connection
    {
        private const int IsoOverTcpPort = 102;

        private readonly string host;
        private readonly Tsap sourceTsap;
        private readonly Tsap destinationTsap;
        private readonly TcpClient client = new TcpClient {NoDelay = true};

        private readonly IValueConverter valueConverter = new DelegatingValueConverter();

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

        public async Task Read(params IDataItem[] dataItems)
        {
            var stream = client.GetStream();
            await stream.WriteAsync(buffer, 0, BuildReadRequest(dataItems)).ConfigureAwait(false);
            ParseReadResponse(dataItems, await ReadTpkt().ConfigureAwait(false));
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

        private int BuildReadRequest(in IReadOnlyList<IDataItem> dataItems)
        {
            ref var read = ref buffer.Struct<ReadRequest>(17);
            read.FunctionCode = FunctionCode.Read;
            read.ItemCount = (byte) dataItems.Count;
            var parameters = MemoryMarshal.Cast<byte, RequestItem>(buffer.AsSpan().Slice(19));
            for (var i = 0; i < dataItems.Count; i++)
            {
                ApplyDataItem(ref parameters[i], dataItems[i]);
            }

            return BuildS7JobRequest(dataItems.Count * 12 + 2, 0);
        }

        private static void ApplyDataItem(ref RequestItem requestItem, in IDataItem dataItem)
        {
            requestItem.Init();
            requestItem.Address = dataItem.Address;
            requestItem.Area = dataItem.Area;
            requestItem.DbNumber = dataItem.DbNumber;
            requestItem.VariableType = dataItem.VariableType;

            if (dataItem.ValueType == typeof(bool))
            {
                requestItem.Count = 1;
            }
            else if (dataItem.ValueType == typeof(short))
            {
                requestItem.Count = 2;
            }
            else if (dataItem.ValueType == typeof(int))
            {
                requestItem.Count = 4;
            }
            else if (dataItem.ValueType == typeof(byte[]))
            {
                requestItem.Count = dataItem.Length;
            }
            else
            {
                throw new Exception($"Unsupported type requested.");
            }
        }

        private int BuildS7JobRequest(in BigEndianShort parameterLength, in BigEndianShort dataLength)
        {
            var len = parameterLength + dataLength + 17; // Error omitted
            buffer.Struct<Tpkt>(0).Init(len);
            buffer.Struct<Data>(4).Init();
            buffer.Struct<Header>(7).Init(MessageType.JobRequest, parameterLength, dataLength);

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

        private void ParseReadResponse(IReadOnlyCollection<IDataItem> dataItems, in int length)
        {
            DumpBuffer(length);

            ref var dt = ref buffer.Struct<Data>(4);
            dt.Assert();

            ref var s7Header = ref buffer.Struct<Header>(7);
            s7Header.Assert(MessageType.AckData);
            if (s7Header.ParamLength != 2) throw new Exception($"Read returned unexpected parameter length {s7Header.ParamLength}");

            ref var response = ref buffer.Struct<ReadRequest>(19);
            response.Assert((byte) dataItems.Count);

            if (length != s7Header.DataLength + 21)
                throw new Exception($"Length of response ({length}) does not match length of fixed part ({s7Header.ParamLength}) and data ({s7Header.DataLength}) of S7 Ack Data.");

            var data = buffer.AsSpan().Slice(21, s7Header.DataLength);
            List<Exception> exceptions = null;

            foreach (var dataItem in dataItems)
            {
                ref var di = ref data.Struct<DataItem>(0);
                if (di.ErrorCode == ReadWriteErrorCode.Success)
                {
                    //Sharp7:
                    //if ((S7ItemRead[1] != TS_ResOctet) && (S7ItemRead[1] != TS_ResReal) && (S7ItemRead[1] != TS_ResBit))
                    //    ItemSize = ItemSize >> 3;

                    var size = di.Count / 8;
                    valueConverter.DecodeDataItemValue(data.Slice(4, size), dataItem, size);

                    data = data.Slice(size + 4);
                }
                else
                {
                    if (exceptions == null) exceptions = new List<Exception>(1);

                    exceptions.Add(
                        new Exception($"Read of dataItem {dataItem} returned {di.ErrorCode}"));
                    data = data.Slice(4);
                }
            }

            if (exceptions != null) throw new AggregateException(exceptions);
        }

        private void DumpBuffer(in int length, [CallerMemberName] string caller = null)
        {
            Console.WriteLine($"{caller}: {string.Join(", ", buffer.Take(length).Select(b => $"{b:X}"))}");
            Console.WriteLine($"{caller}: {string.Join(", ", buffer.Take(length).Select(b => b))}");
        }
    }
}
