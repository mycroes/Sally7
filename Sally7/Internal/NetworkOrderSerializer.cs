using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Sally7.Internal;

internal static class NetworkOrderSerializer
{
    public static void WriteInt16(ref byte destination, short value)
    {
        Unsafe.WriteUnaligned(ref destination,
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(value) : value);
    }

    public static void WriteInt32(ref byte destination, int value)
    {
        Unsafe.WriteUnaligned(ref destination,
            BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(value) : value);
    }
}