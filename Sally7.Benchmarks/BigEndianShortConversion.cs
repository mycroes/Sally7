using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks;

public class BigEndianShortConversion
{
    private readonly int intValue = ushort.MaxValue;

    private readonly byte byteValue = byte.MaxValue;

    private readonly ushort ushortValue = ushort.MaxValue;

    [Benchmark]
    public BigEndianShort Manual_From_Byte()
    {
        return new BigEndianShort { High = 0, Low = byteValue };
    }

    [Benchmark]
    public BigEndianShort Manual_From_UShort()
    {
        return new BigEndianShort { High = (byte)(ushortValue >> 8), Low = (byte)ushortValue };
    }

    [Benchmark]
    public BigEndianShort Manual_From_Int()
    {
        return new BigEndianShort { High = (byte)(intValue >> 8), Low = (byte)intValue };
    }

    [Benchmark]
    public BigEndianShort Unsafe_As_And_BinaryPrimitives_ReverseEndianness_From_Byte()
    {
        var value = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((ushort)byteValue) : byteValue;
        return Unsafe.As<ushort, BigEndianShort>(ref value);
    }

    [Benchmark]
    public BigEndianShort Unsafe_As_And_BinaryPrimitives_ReverseEndianness_From_UShort()
    {
        var value = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(ushortValue) : ushortValue;
        return Unsafe.As<ushort, BigEndianShort>(ref value);
    }

    [Benchmark]
    public BigEndianShort Unsafe_As_And_BinaryPrimitives_ReverseEndianness_From_Int()
    {
        var value = BitConverter.IsLittleEndian
            ? BinaryPrimitives.ReverseEndianness((ushort)intValue)
            : (ushort)intValue;
        return Unsafe.As<ushort, BigEndianShort>(ref value);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BigEndianShort
    {
        public byte High;
        public byte Low;
    }
}