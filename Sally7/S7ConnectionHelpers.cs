using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sally7.Infrastructure;
using Sally7.Protocol;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.Cotp.Messages;
using Sally7.Protocol.IsoOverTcp;
using Sally7.Protocol.S7;
using Sally7.Protocol.S7.Messages;

namespace Sally7
{
    internal static class S7ConnectionHelpers
    {
        public static int BuildConnectRequest(in Span<byte> buffer, in Tsap sourceTsap, in Tsap destinationTsap)
        {
            ref var message = ref buffer.Struct<ConnectionRequestMessage>(4);
            message.Init(PduSizeParameter.PduSize.Pdu512, sourceTsap, destinationTsap);
            var len = 4 + ConnectionRequestMessage.Size;
            buffer.Struct<Tpkt>(0).Init(len);

            DumpBuffer(buffer.Slice(0, len));

            return len;
        }

        public static int BuildCommunicationSetup(in Span<byte> buffer)
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

            DumpBuffer(buffer.Slice(0, len));

            return len;
        }

        public static int BuildReadRequest(in Span<byte> buffer, in IReadOnlyList<IDataItem> dataItems)
        {
            ref var read = ref buffer.Struct<ReadRequest>(17);
            read.FunctionCode = FunctionCode.Read;
            read.ItemCount = (byte) dataItems.Count;
            var parameters = MemoryMarshal.Cast<byte, RequestItem>(buffer.Slice(19));
            for (var i = 0; i < dataItems.Count; i++)
            {
                BuildRequestItem(ref parameters[i], dataItems[i]);
                parameters[i].Count = dataItems[i].ReadCount;
            }

            return BuildS7JobRequest(buffer, dataItems.Count * 12 + 2, 0);
        }

        public static int BuildWriteRequest(in Span<byte> buffer, in IReadOnlyList<IDataItem> dataItems)
        {
            var span = buffer.Slice(17); // Skip header
            span[0] = (byte) FunctionCode.Write;
            span[1] = (byte) dataItems.Count;
            var parameters = MemoryMarshal.Cast<byte, RequestItem>(span.Slice(2));
            var fnParameterLength = dataItems.Count * 12 + 2;
            var dataLength = 0;
            var data = span.Slice(fnParameterLength);
            for (var i = 0; i < dataItems.Count; i++)
            {
                BuildRequestItem(ref parameters[i], dataItems[i]);

                ref var dataItem = ref data.Struct<DataItem>(0);
                dataItem.ErrorCode = 0;
                dataItem.TransportSize = dataItems[i].TransportSize;

                var length = dataItems[i].WriteValue(data.Slice(4));
                parameters[i].Count = length;
                dataItem.Count = dataItem.TransportSize.IsSizeInBytes() ? length : length << 3;

                length += 4; // Add sizeof(DataItem)
                if (length % 2 == 1)
                {
                    data[++length] = 0;
                }
                dataLength += length;

                data = data.Slice(length);
            }

            return BuildS7JobRequest(buffer, fnParameterLength, dataLength);
        }

        private static void BuildRequestItem(ref RequestItem requestItem, in IDataItem dataItem)
        {
            requestItem.Init();
            requestItem.Address = dataItem.Address;
            requestItem.Area = dataItem.Area;
            requestItem.DbNumber = dataItem.DbNumber;
            requestItem.VariableType = dataItem.VariableType;
        }

        private static int BuildS7JobRequest(in Span<byte> buffer, in BigEndianShort parameterLength, in BigEndianShort dataLength)
        {
            var len = parameterLength + dataLength + 17; // Error omitted
            buffer.Struct<Tpkt>(0).Init(len);
            buffer.Struct<Data>(4).Init();
            buffer.Struct<Header>(7).Init(MessageType.JobRequest, parameterLength, dataLength);

            DumpBuffer(buffer.Slice(0, len));

            return len;
        }

        public static void ParseConnectionConfirm(in ReadOnlySpan<byte> buffer)
        {
            DumpBuffer(buffer);

            var fixedPartLength = buffer[5];
            if (fixedPartLength < ConnectionConfirm.Size)
                throw new Exception("Received data is smaller than Connection Confirm fixed part.");

            ref readonly var cc = ref buffer.Struct<ConnectionConfirm>(5);
            cc.Assert();

            // Analyze returned parameters?
        }

        public static void ParseCommunicationSetup(in ReadOnlySpan<byte> buffer, out int pduSize)
        {
            DumpBuffer(buffer);
            if (buffer.Length < 19 + CommunicationSetup.Size) throw new Exception("Received data is smaller than TPKT + DT PDU + S7 header + S7 communication setup size.");
            ref readonly var dt = ref buffer.Struct<Data>(4);
            dt.Assert();

            ref readonly var s7Header = ref buffer.Struct<Header>(7);
            s7Header.Assert(MessageType.AckData);

            ref readonly var s7CommunicationSetup = ref buffer.Struct<CommunicationSetup>(19);
            s7CommunicationSetup.Assert(FunctionCode.CommunicationSetup);

            pduSize = s7CommunicationSetup.PduSize;
        }

        public static void ParseReadResponse(in ReadOnlySpan<byte> buffer, in IReadOnlyCollection<IDataItem> dataItems)
        {
            DumpBuffer(buffer);

            ref readonly var dt = ref buffer.Struct<Data>(4);
            dt.Assert();

            ref readonly var s7Header = ref buffer.Struct<Header>(7);
            s7Header.Assert(MessageType.AckData);
            if (s7Header.ParamLength != 2) throw new Exception($"Read returned unexpected parameter length {s7Header.ParamLength}");

            ref readonly var response = ref buffer.Struct<ReadRequest>(19);
            response.Assert((byte) dataItems.Count);

            if (buffer.Length != s7Header.DataLength + 21)
                throw new Exception($"Length of response ({buffer.Length}) does not match length of fixed part ({s7Header.ParamLength}) and data ({s7Header.DataLength}) of S7 Ack Data.");

            var data = buffer.Slice(21, s7Header.DataLength);
            List<Exception> exceptions = null;

            var offset = 0;
            foreach (var dataItem in dataItems)
            {
                // If the last item is odd length, there's no additional 0 padded. Slicing at the end of the loop
                // causes ArgumentOutOfRange in such conditions.
                data = data.Slice(offset);

                ref readonly var di = ref data.Struct<DataItem>(0);
                if (di.ErrorCode == ReadWriteErrorCode.Success)
                {
                    var size = di.TransportSize.IsSizeInBytes() ? (int) di.Count : di.Count >> 3;
                    dataItem.ReadValue(data.Slice(4, size));

                    // Odd sizes are padded in the message
                    if (size % 2 == 1) size++;

                    offset = size + 4;
                }
                else
                {
                    if (exceptions == null) exceptions = new List<Exception>(1);

                    exceptions.Add(
                        new Exception($"Read of dataItem {dataItem} returned {di.ErrorCode}"));
                    offset = 4;
                }
            }

            if (exceptions != null) throw new AggregateException(exceptions);
        }

        public static void ParseWriteResponse(in ReadOnlySpan<byte> buffer, in IReadOnlyList<IDataItem> dataItems)
        {
            DumpBuffer(buffer);

            ref readonly var dt = ref buffer.Struct<Data>(4);
            dt.Assert();

            ref readonly var s7Header = ref buffer.Struct<Header>(7);
            s7Header.Assert(MessageType.AckData);
            if (s7Header.ParamLength != 2)
                throw new Exception($"Write returned unexpected parameter length {s7Header.ParamLength}");

            if ((FunctionCode) buffer[19] != FunctionCode.Write)
                throw new Exception($"Expected FunctionCode {FunctionCode.Write}, received {(FunctionCode) buffer[19]}.");
            if (buffer[20] != dataItems.Count)
                throw new Exception($"Expected {dataItems.Count} items in write response, received {buffer[20]}.");

            if (buffer.Length != s7Header.DataLength + 21)
                throw new Exception(
                    $"Length of response ({buffer.Length}) does not match length of fixed part ({s7Header.ParamLength}) and data ({s7Header.DataLength}) of S7 Ack Data.");

            var errorCodes = MemoryMarshal.Cast<byte, ReadWriteErrorCode>(buffer.Slice(21));
            List<Exception> exceptions = null;

            for (var i = 0; i < dataItems.Count; i++)
            {
                if (errorCodes[i] == ReadWriteErrorCode.Success) continue;

                if (exceptions == null) exceptions = new List<Exception>(1);
                exceptions.Add(new Exception($"Read of dataItem {dataItems[i]} returned {errorCodes[i]}"));
            }
        }

        private static void DumpBuffer(in ReadOnlySpan<byte> buffer, [CallerMemberName] in string caller = null)
        {
            //Console.WriteLine($"{caller}: {string.Join(", ", buffer.Take(length).Select(b => $"{b:X}"))}");
            //Console.WriteLine($"{caller}: {string.Join(", ", buffer.Take(length).Select(b => b))}");
        }
    }
}