using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sally7.Tests;

internal static class TestValues
{
    public static IEnumerable<byte> ByteData =>
        Enumerable.Range(byte.MinValue, byte.MaxValue).Select(x => (byte)x);

    public static IEnumerable<sbyte> SByteData =>
        Enumerable.Range(sbyte.MinValue, sbyte.MaxValue).Select(x => (sbyte)x);


    public static IEnumerable<short> Int16Data => ShortValues;

    public static IEnumerable<ushort> UInt16Data =>
        ShortValues.Select(x => (ushort)x);

    public static IEnumerable<int> Int32Data => UIntValues.Select(x => (int)x);

    public static IEnumerable<uint> UInt32Data => UIntValues;

    public static IEnumerable<float> SingleData
    {
        get
        {
            float[] values =
            [
                0,
                1,
                0.1f,
                123.45f,
                float.MinValue,
                float.MaxValue,
            ];

            return values;
        }
    }

    public static IEnumerable<long> Int64Data => ULongValues.Select(x => (long) x);

    public static IEnumerable<ulong> UInt64Data => ULongValues;

    public static IEnumerable<double> DoubleData
    {
        get
        {
            double[] values =
            [
                0,
                1,
                0.1,
                123.45,
                0.0000001,
                (double) ulong.MaxValue * 33,
                double.MinValue,
                double.MaxValue,
            ];

            return values;
        }
    }

    private static readonly short[] ShortValues =
    [
        0,
        1,
        123,
        12345,
        -1,
        -123,
        -12345,
        1 << 8,
        1 | 2 << 8,
        short.MinValue,
        short.MaxValue
    ];

    private static readonly uint[] UIntValues =
    [
        uint.MinValue,
        uint.MaxValue,
        1,
        123,
        12345,
        1 << 8,
        1 << 16,
        1 << 24,
        1 | 2 << 8,
        1 | 2 << 8 | 3 << 16,
        1 | 2 << 8 | 3 << 24,
        1 | 2 << 8 | 3 << 16 | 4 << 24,
        0xf,
        0xf << 8,
        0xf << 16,
        0xf << 24,
    ];

    private static readonly ulong[] ULongValues = UIntValues.Select(x => (ulong)x)
        .SelectMany(x => new[] { x, x << 8, x << 16, x << 24, x << 32 })
        .Concat(UIntValues.SelectMany(_ => UIntValues, (a, b) => (ulong)a << 32 | b))
        .Concat(UIntValues.SelectMany(_ => UIntValues, (a, b) => a | (ulong)b << 32)).Distinct().ToArray();
}