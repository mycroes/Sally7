using System;
using System.Buffers.Binary;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sally7.Internal;
using Sally7.Protocol.S7.Messages;

namespace Sally7.Benchmarks.Serialization
{
    public class SerializeJobRequestHeader
    {
        private const int JobRequestHeader1 = 0x32 << 24 // Protocol ID
            | (byte)MessageType.JobRequest << 16; // Message type

        private const ushort PduRef = 1 << 8;

        private static ulong JobRequestHeaderLong = 0x32L << 56 | (long)MessageType.JobRequest << 48 | PduRef << 16;

        private readonly byte[] buffer = new byte[10];

        [Benchmark(Baseline = true)]
        public uint WriteFieldsOneByOne()
        {
            ref var start = ref MemoryMarshal.GetReference(buffer.AsSpan());
            return WriteFieldsOneByOne(ref start, 10, 20);
        }

        [Benchmark]
        public uint WriteLongThenShort()
        {
            ref var start = ref MemoryMarshal.GetReference(buffer.AsSpan());
            return WriteLongThenShort(ref start, 10, 20);
        }

        [Benchmark]
        public uint WriteArray()
        {
            ref var start = ref MemoryMarshal.GetReference(buffer.AsSpan());
            return WriteArray(ref start, 10, 20);
        }

        [Benchmark]
        public uint WriteStruct()
        {
            ref var start = ref MemoryMarshal.GetReference(buffer.AsSpan());
            return WriteStruct(ref start, 10, 20);
        }

        [GlobalCleanup]
        public void VerifyBuffer()
        {
            byte[] expected = { 0x32, 1, 0, 0, 1, 0, 0, 10, 0, 20 };
            if (!buffer.SequenceEqual(expected))
            {
                throw new Exception($"""
                    Buffer contents are invalid.
                    Expected: {BitConverter.ToString(expected)}
                    Received: {BitConverter.ToString(buffer)}
                    """);
            }
        }

        public static uint WriteFieldsOneByOne(ref byte destination, int paramLength, int dataLength)
        {
            WriteUInt32(ref destination, JobRequestHeader1);
            WriteUInt16(ref destination.GetOffset(4), PduRef); // Ignore PDU ref, leave to request executor.
            WriteUInt16(ref destination.GetOffset(6), (ushort)paramLength);
            WriteUInt16(ref destination.GetOffset(8), (ushort)dataLength);

            return 10;
        }

        public static uint WriteLongThenShort(ref byte destination, int paramLength, int dataLength)
        {
            var header = JobRequestHeaderLong | (ushort)paramLength;

            Unsafe.WriteUnaligned(ref destination,
                    BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(header) : header);
            WriteUInt16(ref destination.GetOffset(8), (ushort)dataLength);

            return 10;
        }

        public static unsafe uint WriteArray(ref byte destination, int paramLength, int dataLength)
        {
            var request = stackalloc ushort[5];
            request[0] = 0x32_01;
            request[1] = 0;
            request[2] = PduRef;
            request[3] = (ushort)paramLength;
            request[4] = (ushort)dataLength;

            if (BitConverter.IsLittleEndian)
            {
                request[0] = BinaryPrimitives.ReverseEndianness(request[0]);
                request[2] = BinaryPrimitives.ReverseEndianness(request[2]);
                request[3] = BinaryPrimitives.ReverseEndianness(request[3]);
                request[4] = BinaryPrimitives.ReverseEndianness(request[4]);
            }

            Unsafe.CopyBlockUnaligned(ref destination, ref Unsafe.AsRef<byte>(request), 10);

            return 10;
        }

        public static uint WriteStruct(ref byte destination, int paramLength, int dataLength)
        {
            var header = BitConverter.IsLittleEndian
                ? new Header
                {
                    ProtocolId = 0x32,
                    MessageType = (byte)MessageType.JobRequest,
                    Reserved = 0,
                    PduRef = BinaryPrimitives.ReverseEndianness(PduRef),
                    ParamLength = BinaryPrimitives.ReverseEndianness((ushort)paramLength),
                    DataLength = BinaryPrimitives.ReverseEndianness((ushort)dataLength)
                }
                : new Header
                {
                    ProtocolId = 0x32,
                    MessageType = (byte)MessageType.JobRequest,
                    Reserved = 0,
                    PduRef = PduRef,
                    ParamLength = (ushort)paramLength,
                    DataLength = (ushort)dataLength
                };

            Unsafe.WriteUnaligned(ref destination, header);

            return 10;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Header
        {
            public byte ProtocolId;
            public byte MessageType;
            public ushort Reserved;
            public ushort PduRef;
            public ushort ParamLength;
            public ushort DataLength;
        }

        public static uint WriteUInt16(ref byte destination, ushort value)
        {
            NetworkOrderSerializer.WriteUInt16(ref destination, value);
            return sizeof(short);
        }

        public static uint WriteUInt32(ref byte destination, uint value)
        {
            NetworkOrderSerializer.WriteUInt32(ref destination, value);
            return sizeof(int);
        }

        private static class NetworkOrderSerializer
        {
            public static void WriteUInt16(ref byte destination, ushort value)
            {
                Unsafe.WriteUnaligned(ref destination,
                    BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(value) : value);
            }

            public static void WriteUInt32(ref byte destination, uint value)
            {
                Unsafe.WriteUnaligned(ref destination,
                    BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(value) : value);
            }
        }
    }
}
