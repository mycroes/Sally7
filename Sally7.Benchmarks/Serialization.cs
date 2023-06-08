using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sally7.Protocol;
using Sally7.Protocol.S7;

namespace Sally7.Benchmarks;

public class Serialization
{
    private readonly byte[] buffer = new byte[31];

    [Benchmark]
    public void MemoryMarshal_Cast()
    {
        var span = buffer.AsSpan();

        ref var tpkt = ref MemoryMarshal.Cast<byte, Tpkt>(span)[0];
        tpkt.Version = 3;
        tpkt.Reserved = 0;
        tpkt.Length = 31;

        ref var data = ref MemoryMarshal.Cast<byte, Data>(span.Slice(4))[0];
        data.Length = 2;
        data.DataIdentifier = 0b1111_0000;
        data.PduNumberAndEot = 0b1_000_0000;

        ref var s7Header = ref MemoryMarshal.Cast<byte, S7Header>(span.Slice(7))[0];
        s7Header.ProtocolId = 0x32;
        s7Header.MessageType = MessageType.JobRequest;
        s7Header.Reserved = 0;
        s7Header.PduRef = 0x0101;
        s7Header.ParamLength = 14;
        s7Header.DataLength = 0;

        ref var rr = ref MemoryMarshal.Cast<byte, ReadRequest>(span.Slice(17))[0];
        rr.FunctionCode = FunctionCode.Read;
        rr.ItemCount = 1;

        ref var ri = ref MemoryMarshal.Cast<byte, RequestItem>(span.Slice(19))[0];
        ri.Spec = 0x12;
        ri.Length = 10;
        ri.SyntaxId = AddressingMode.S7Any;
        ri.VariableType = VariableType.Byte;
        ri.Count = 2;
        ri.DbNumber = 2000;
        ri.Area = Area.DataBlock;
        ri.Address = new Address { High = 0, Mid = 0, Low = 20 };
    }

    [Benchmark]
    public void Unsafe_WriteUnaligned()
    {
        var span = buffer.AsSpan();
        ref var start = ref MemoryMarshal.GetReference(span);
        // TPKT
        Unsafe.WriteUnaligned(ref start, (byte) 3);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 1), 0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 2),
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((ushort)31) : (ushort)31);

        // Data
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 4), (byte) 2);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 5), (byte) 0b1111_0000);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 6), (byte) 0b1_000_0000);

        // S7 header
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 7), (byte) 0x32);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 8), (byte) MessageType.JobRequest);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 9), (ushort) 0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 11), (ushort) 0x0101);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 13),
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((ushort) 14) : (ushort) 14);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 15), (ushort) 0);

        // Read request
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 17), (byte) FunctionCode.Read);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 18), (byte) 1);

        // Request item
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 19), (byte) 0x12);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 20), (byte) 10);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 21), (byte) AddressingMode.S7Any);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 22), (byte) VariableType.Byte);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 23),
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((ushort)2) : (ushort)2);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 25),
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((ushort)2000) : (ushort)2000);
        // Hack: Big endian int uses last 3 bytes for lower range, so we can just put the address in as int
        // and overwrite the first byte afterwards.
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 27),
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(20u) : 20u);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 27), (byte) Area.DataBlock);
    }

    [Benchmark]
    public void Unsafe_As_Struct()
    {
        var span = buffer.AsSpan();
        ref var start = ref MemoryMarshal.GetReference(span);
        ref var tpkt = ref Unsafe.As<byte, Tpkt>(ref start);
        tpkt.Version = 3;
        tpkt.Reserved = 0;
        tpkt.Length = 31;

        ref var data = ref Unsafe.As<byte, Data>(ref Unsafe.Add(ref start, 4));
        data.Length = 2;
        data.DataIdentifier = 0b1111_0000;
        data.PduNumberAndEot = 0b1_000_0000;

        ref var s7Header = ref Unsafe.As<byte, S7Header>(ref Unsafe.Add(ref start, 7));
        s7Header.ProtocolId = 0x32;
        s7Header.MessageType = MessageType.JobRequest;
        s7Header.Reserved = 0;
        s7Header.PduRef = 0x0101;
        s7Header.ParamLength = 14;
        s7Header.DataLength = 0;

        ref var rr = ref Unsafe.As<byte, ReadRequest>(ref Unsafe.Add(ref start, 17));
        rr.FunctionCode = FunctionCode.Read;
        rr.ItemCount = 1;

        ref var ri = ref Unsafe.As<byte, RequestItem>(ref Unsafe.Add(ref start, 19));
        ri.Spec = 0x12;
        ri.Length = 10;
        ri.SyntaxId = AddressingMode.S7Any;
        ri.VariableType = VariableType.Byte;
        ri.Count = 2;
        ri.DbNumber = 2000;
        ri.Area = Area.DataBlock;
        ri.Address = new Address { High = 0, Mid = 0, Low = 20 };
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct Tpkt
    {
        public byte Version;
        public byte Reserved;
        public BigEndianShort Length;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct Data
    {
        public byte Length;
        public byte DataIdentifier;
        public byte PduNumberAndEot;
    }

    private enum MessageType : byte
    {
        JobRequest = 0x01,
        Ack = 0x02,
        AckData = 0x03,
        UserData = 0x07
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct S7Header
    {
        public byte ProtocolId;
        public MessageType MessageType;
        public BigEndianShort Reserved;
        public short PduRef;
        public BigEndianShort ParamLength;
        public BigEndianShort DataLength;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ReadRequest
    {
        public FunctionCode FunctionCode;
        public byte ItemCount;
    }

    private enum FunctionCode : byte
    {
        Read = 0x04,
        Write = 0x05,
        CommunicationSetup = 0xf0
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct RequestItem
    {
        public byte Spec;
        public byte Length;
        public AddressingMode SyntaxId;
        public VariableType VariableType;
        public BigEndianShort Count;
        public BigEndianShort DbNumber;
        public Area Area;
        public Address Address;
    }

    private enum AddressingMode : byte
    {
        S7Any = 0x10, // S7-Any pointer (regular addressing) memory+variable length+offset
        DriveES = 0xa2, // Drive-ES-Any seen on Drive ES Starter with routing over S7
        SubItem = 0xb0, // Special DB addressing for S400 (subitem read/write)
        Symbolic = 0xb2 // S1200/S1500? Symbolic addressing mode
    }
}