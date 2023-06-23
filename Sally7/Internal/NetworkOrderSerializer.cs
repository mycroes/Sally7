using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Sally7.Internal;

internal static class NetworkOrderSerializer
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