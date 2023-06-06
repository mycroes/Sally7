using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sally7.Protocol;

namespace Sally7.Benchmarks;

public class Serialization
{
    private readonly byte[] buffer = new byte[10];

    [Benchmark]
    public void MemoryMarshal_Cast()
    {
        var span = buffer.AsSpan();

        ref var tpkt = ref MemoryMarshal.Cast<byte, Tpkt>(span)[0];
        tpkt.Version = 3;
        tpkt.Reserved = 0;
        tpkt.Length = 21;

        ref var cr = ref MemoryMarshal.Cast<byte, ConnectionRequest>(span.Slice(4))[0];
        cr.ConnectionRequestAndCredit = 0b1110_0000;
        cr.DestinationReference = 0;
        cr.SourceReference = 0;
        cr.ClassAndOption = 0;
    }

    [Benchmark]
    public void Unsafe_WriteUnaligned()
    {
        var span = buffer.AsSpan();
        ref var start = ref MemoryMarshal.GetReference(span);
        Unsafe.WriteUnaligned(ref start, (byte) 3);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 1), 0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 2),
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((ushort)21) : (ushort)21);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 4), (byte) 0b1110_0000);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 5), 0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref start, 9), (byte) 0);
    }

    [Benchmark]
    public void Unsafe_As_Struct()
    {
        var span = buffer.AsSpan();
        ref var start = ref MemoryMarshal.GetReference(span);
        ref var tpkt = ref Unsafe.As<byte, Tpkt>(ref start);
        tpkt.Version = 3;
        tpkt.Reserved = 0;
        tpkt.Length = 21;

        ref var cr = ref Unsafe.As<byte, ConnectionRequest>(ref Unsafe.Add(ref start, 4));
        cr.ConnectionRequestAndCredit = 0b1110_0000;
        cr.DestinationReference = 0;
        cr.SourceReference = 0;
        cr.ClassAndOption = 0;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct Tpkt
    {
        public byte Version;
        public byte Reserved;
        public BigEndianShort Length;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ConnectionRequest
    {
        public byte ConnectionRequestAndCredit;
        public ushort DestinationReference;
        public ushort SourceReference;
        public byte ClassAndOption;

        public void Init()
        {
            ConnectionRequestAndCredit = 0b1110_0000;
            ClassAndOption = 0; // Only class 0 is supported when using TPKT over TCP
            DestinationReference = default; // Anything
            SourceReference = default; // Anything
        }
    }
}