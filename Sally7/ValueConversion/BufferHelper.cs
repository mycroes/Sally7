using Sally7.Internal;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sally7.ValueConversion;

internal class BufferHelper
{
    public static int CopyBytes(ReadOnlySpan<byte> input, Span<byte> output, int numberOfBytes)
    {
        ref var destination = ref MemoryMarshal.GetReference(output);
        ref var source = ref MemoryMarshal.GetReference(input);

        var offset = 0u;
        while (offset <= numberOfBytes - sizeof(ulong))
        {
            var value = Unsafe.ReadUnaligned<ulong>(ref source.GetOffset(offset));
            Unsafe.WriteUnaligned(ref destination.GetOffset(offset), value);

            offset += sizeof(ulong);
        }

        if (offset <= input.Length - sizeof(uint))
        {
            var value = Unsafe.ReadUnaligned<uint>(ref source.GetOffset(offset));
            Unsafe.WriteUnaligned(ref destination.GetOffset(offset), value);

            offset += sizeof(uint);
        }

        if (offset <= numberOfBytes - sizeof(ushort))
        {
            var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
            Unsafe.WriteUnaligned(ref destination.GetOffset(offset), value);

            offset += sizeof(ushort);
        }

        if (offset < numberOfBytes)
        {
            var value = Unsafe.ReadUnaligned<byte>(ref source.GetOffset(offset));
            Unsafe.WriteUnaligned(ref destination.GetOffset(offset), value);

            offset += sizeof(byte);
        }
        return (int)offset;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CopyAndAlign64Bit(ReadOnlySpan<byte> input, Span<byte> output, int numberOfItems)
    {
        ref var destination = ref MemoryMarshal.GetReference(output);
        ref var source = ref MemoryMarshal.GetReference(input);

        var limit = (uint) numberOfItems * sizeof(ulong);

        var offset = 0u;
        while (offset < limit)
        {
            var value = Unsafe.ReadUnaligned<ulong>(ref source.GetOffset(offset));
            NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), value);

            offset += sizeof(ulong);
        }

        return (int)offset;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CopyAndAlign32Bit(ReadOnlySpan<byte> input, Span<byte> output, int numberOfItems)
    {
        ref var destination = ref MemoryMarshal.GetReference(output);
        ref var source = ref MemoryMarshal.GetReference(input);

        var limit = (uint) numberOfItems * sizeof(uint);

        var offset = 0u;
        while (offset < limit)
        {
            var value = Unsafe.ReadUnaligned<uint>(ref source.GetOffset(offset));
            NetworkOrderSerializer.WriteUInt32(ref destination.GetOffset(offset), value);

            offset += sizeof(uint);
        }

        return (int)offset;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CopyAndAlign16Bit(ReadOnlySpan<byte> input, Span<byte> output, int numberOfItems)
    {
        ref var destination = ref MemoryMarshal.GetReference(output);
        ref var source = ref MemoryMarshal.GetReference(input);

        var limit = (uint) numberOfItems * sizeof(ushort);

        var offset = 0u;
        while (offset < limit)
        {
            var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
            NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(offset), value);

            offset += sizeof(ushort);
        }

        return (int)offset;
    }
}