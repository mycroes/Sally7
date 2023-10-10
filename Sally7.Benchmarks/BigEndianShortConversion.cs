using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks;

public class BigEndianShortConversion
{
    private readonly int _intValue = ushort.MaxValue;

    private readonly byte _byteValue = byte.MaxValue;

    private readonly ushort _ushortValue = ushort.MaxValue;

    [Benchmark]
    public BigEndianShort Manual_From_Byte()
    {
        return new BigEndianShort { High = 0, Low = _byteValue };
    }

    [Benchmark]
    public BigEndianShort Manual_From_UShort()
    {
        return new BigEndianShort { High = (byte)(_ushortValue >> 8), Low = (byte)_ushortValue };
    }

    [Benchmark]
    public BigEndianShort Manual_From_Int()
    {
        return new BigEndianShort { High = (byte)(_intValue >> 8), Low = (byte)_intValue };
    }

    [Benchmark]
    public BigEndianShort Unsafe_As_And_BinaryPrimitives_ReverseEndianness_From_Byte()
    {
        var value = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((ushort)_byteValue) : _byteValue;
        return Unsafe.As<ushort, BigEndianShort>(ref value);
    }

    [Benchmark]
    public BigEndianShort Unsafe_As_And_BinaryPrimitives_ReverseEndianness_From_UShort()
    {
        var value = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(_ushortValue) : _ushortValue;
        return Unsafe.As<ushort, BigEndianShort>(ref value);
    }

    [Benchmark]
    public BigEndianShort Unsafe_As_And_BinaryPrimitives_ReverseEndianness_From_Int()
    {
        var value = BitConverter.IsLittleEndian
            ? BinaryPrimitives.ReverseEndianness((ushort)_intValue)
            : (ushort)_intValue;
        return Unsafe.As<ushort, BigEndianShort>(ref value);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BigEndianShort
    {
        public byte High;
        public byte Low;
    }
}