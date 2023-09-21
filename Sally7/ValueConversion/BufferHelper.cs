using Sally7.Internal;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sally7.ValueConversion;

internal class BufferHelper
{
    /// <summary>
    /// Copies a buffer from input to output and reverses endianness if needed.
    /// </summary>
    /// <param name="input">The input buffer.</param>
    /// <param name="output">The output buffer.</param>
    /// <param name="numberOfItems">The number of items to copy.</param>
    /// <param name="elementSize">The size of the elements to copy.</param>
    /// <returns>The number of bytes copied.</returns>
    public static int CopyAndFix(ReadOnlySpan<byte> input, Span<byte> output, int numberOfItems, int elementSize)
    {
        ref var destination = ref MemoryMarshal.GetReference(output);
        ref var source = ref MemoryMarshal.GetReference(input);

        var offset = 0u;
        var limit = numberOfItems * elementSize;
        switch (elementSize)
        {
            case sizeof(ulong):
                while (offset <= limit - sizeof(ulong))
                {
                    var value = Unsafe.ReadUnaligned<ulong>(ref source.GetOffset(offset));
                    NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), value);

                    offset += sizeof(ulong);
                }

                break;
            case sizeof(uint):
                while (offset <= limit - sizeof(uint))
                {
                    var value = Unsafe.ReadUnaligned<uint>(ref source.GetOffset(offset));
                    NetworkOrderSerializer.WriteUInt32(ref destination.GetOffset(offset), value);

                    offset += sizeof(uint);
                }

                break;
            case sizeof(ushort):
                while (offset <= limit - sizeof(ushort))
                {
                    var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
                    NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(offset), value);

                    offset += sizeof(ushort);
                }

                break;

            case sizeof(byte):
                while (offset <= limit - sizeof(ulong))
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

                if (offset <= limit - sizeof(ushort))
                {
                    var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
                    Unsafe.WriteUnaligned(ref destination.GetOffset(offset), value);

                    offset += sizeof(ushort);
                }

                if (offset < limit)
                {
                    var value = Unsafe.ReadUnaligned<byte>(ref source.GetOffset(offset));
                    Unsafe.WriteUnaligned(ref destination.GetOffset(offset), value);

                    offset += sizeof(byte);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(elementSize));
        }

        return (int)offset;
    }
}