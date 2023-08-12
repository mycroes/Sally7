using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Sally7.Internal;

namespace Sally7.ValueConversion
{
    internal static class ToS7Conversions
    {
        public static Delegate GetConverter<TValue>(int length)
        {
            if (typeof(TValue).IsPrimitive || typeof(TValue).IsEnum)
            {
                return Unsafe.SizeOf<TValue>() switch
                {
                    sizeof(long) => new ConvertToS7<long>(ConvertFromLong),
                    sizeof(int) => new ConvertToS7<int>(ConvertFromInt),
                    sizeof(short) => new ConvertToS7<short>(ConvertFromShort),
                    sizeof(byte) => new ConvertToS7<byte>(ConvertFromByte),
                    _ => throw new NotImplementedException(),
                };
            }

            if (typeof(TValue) == typeof(bool[]))
                return new ConvertToS7<bool[]>(ConvertFromBoolArray);

            if (typeof(TValue).IsArray)
            {
                var elementType = typeof(TValue).GetElementType() ??
                    throw new Exception($"Type {typeof(TValue)} doesn't have an ElementType.");

                if (elementType.IsPrimitive || elementType.IsEnum)
                {
                    return ConversionHelper.SizeOf(elementType) switch
                    {
                        sizeof(long) => new ConvertToS7<long[]>(ConvertFromLongArray),
                        sizeof(int) => new ConvertToS7<int[]>(ConvertFromIntArray),
                        sizeof(short) => new ConvertToS7<short[]>(ConvertFromShortArray),
                        sizeof(byte) => new ConvertToS7<byte[]>(ConvertFromByteArray),
                        _ => throw new NotImplementedException(),
                    };
                }
            }

            if (typeof(TValue) == typeof(string)) return new ConvertToS7<string>(ConvertFromString);

            throw new NotImplementedException();
        }

        private static int ConvertFromLong(long value, int length, Span<byte> output)
        {
            BinaryPrimitives.WriteInt64BigEndian(output, value);

            return sizeof(long);
        }

        private static int ConvertFromLongArray(long[]? value, int length, Span<byte> output)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Value can't be null.");

            return CopyBuffer(Unsafe.As<long[], byte[]>(ref value), output);
        }

        private static int ConvertFromInt(int value, int length, Span<byte> output)
        {
            BinaryPrimitives.WriteInt32BigEndian(output, value);

            return sizeof(int);
        }

        private static int ConvertFromIntArray(int[]? value, int length, Span<byte> output)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Value can't be null.");

            return CopyBuffer(Unsafe.As<int[], byte[]>(ref value), output);
        }

        private static int ConvertFromShort(short value, int length, Span<byte> output)
        {
            BinaryPrimitives.WriteInt16BigEndian(output, value);

            return sizeof(short);
        }

        private static int ConvertFromShortArray(short[]? value, int length, Span<byte> output)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Value can't be null.");

            return CopyBuffer(Unsafe.As<short[], byte[]>(ref value), output);
        }

        private static int ConvertFromByte(byte value, int length, Span<byte> output)
        {
            output[0] = value;

            return sizeof(byte);
        }

        private static int ConvertFromByteArray(byte[]? value, int length, Span<byte> output)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Value can't be null.");

            return CopyBuffer(value, output);
        }

        private static int ConvertFromBoolArray(bool[]? value, int length, Span<byte> output)
        {
            if (value is null)
            {
                Throw();
                [DoesNotReturn]
                static void Throw() => throw new ArgumentNullException(nameof(value), "Value can't be null");
            }

            length = (length + 7) >> 3;     // (length + 7) / 8

            int outputIdx = 0;
            int bitIdx = 0;

            foreach (bool b in value)
            {
                if (b)
                {
                    output[outputIdx] |= (byte)(1 << bitIdx);
                }
                else
                {
                    output[outputIdx] &= (byte)~(1 << bitIdx);
                }

                bitIdx++;

                if ((bitIdx & 7) == 0)
                {
                    outputIdx++;
                    bitIdx = 0;
                }
            }

            return length;
        }

        private static int ConvertFromString(string? value, int length, Span<byte> output)
        {
            if (value == null)
            {
                output[1] = 0;
                output[0] = (byte) length;

                return 2;
            }

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            var maxByteCount = Encoding.ASCII.GetMaxByteCount(value.Length);
            Span<byte> span = maxByteCount <= 256
                ? stackalloc byte[256]
                : new byte[maxByteCount];

            var written = Encoding.ASCII.GetBytes(value, span);
            written = Math.Min(written, length);
            span = span.Slice(0, written);

            output[1] = (byte) span.Length;
            output[0] = (byte) length;
            span.CopyTo(output.Slice(2));

            return span.Length + 2;
#else
            var bytes = Encoding.ASCII.GetBytes(value);
            var span = bytes.AsSpan();
            if (span.Length > length) span = span.Slice(0, length);

            output[1] = (byte) span.Length;
            output[0] = (byte) length;
            span.CopyTo(output.Slice(2));

            return span.Length + 2;
#endif
        }

        private static int CopyBuffer(ReadOnlySpan<byte> input, Span<byte> output)
        {
            ref var destination = ref MemoryMarshal.GetReference(output);
            ref var source = ref MemoryMarshal.GetReference(input);

            var offset = 0u;
            while (offset + sizeof(ulong) <= input.Length)
            {
                var value = Unsafe.ReadUnaligned<ulong>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), value);

                offset += sizeof(ulong);
            }

            if (offset + sizeof(uint) <= input.Length)
            {
                var value = Unsafe.ReadUnaligned<uint>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt32(ref destination.GetOffset(offset), value);

                offset += sizeof(uint);
            }

            if (offset + sizeof(ushort) <= input.Length)
            {
                var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(offset), value);

                offset += sizeof(ushort);
            }

            if (offset < input.Length)
            {
                var value = Unsafe.ReadUnaligned<byte>(ref source.GetOffset(offset));
                Unsafe.WriteUnaligned(ref destination.GetOffset(offset), value);

                offset += sizeof(byte);
            }

            return (int)offset;
        }
    }
}