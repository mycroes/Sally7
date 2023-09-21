using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sally7.ValueConversion
{
    internal static class FromS7Conversions
    {
        public static Delegate GetConverter<TValue>(int length)
        {
            if (typeof(TValue).IsPrimitive || typeof(TValue).IsEnum)
            {
                return Unsafe.SizeOf<TValue>() switch
                {
                    sizeof(long) => new ConvertFromS7<long>(ConvertToLong),
                    sizeof(int) => new ConvertFromS7<int>(ConvertToInt),
                    sizeof(short) => new ConvertFromS7<short>(ConvertToShort),
                    sizeof(byte) => new ConvertFromS7<byte>(ConvertToByte),
                    _ => throw new NotImplementedException(),
                };
            }

            if (typeof(TValue) == typeof(bool[]))
                return new ConvertFromS7<bool[]>(ConvertToBoolArray);

            if (typeof(TValue).IsArray)
            {
                var elementType = typeof(TValue).GetElementType() ?? throw new Exception(
                    $"Type {typeof(TValue)} doesn't have an ElementType.");

                if (elementType.IsPrimitive || elementType.IsEnum)
                {
                    return ConversionHelper.SizeOf(elementType) switch
                    {
                        sizeof(long) => new ConvertFromS7<long[]>(ConvertToLongArray),
                        sizeof(int) => new ConvertFromS7<int[]>(ConvertToIntArray),
                        sizeof(short) => new ConvertFromS7<short[]>(ConvertToShortArray),
                        sizeof(byte) => new ConvertFromS7<byte[]>(ConvertToByteArray),
                        _ => throw new NotImplementedException(),
                    };
                }
            }

            if (typeof(TValue) == typeof(string)) return new ConvertFromS7<string>(ConvertToString);

            throw new NotImplementedException();
        }

        private static void ConvertToLong(ref long value, ReadOnlySpan<byte> input, int length)
            => value = BinaryPrimitives.ReadInt64BigEndian(input);

        private static void ConvertToLongArray(ref long[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= new long[length];

            BufferHelper.CopyAndFix(input, Unsafe.As<long[], byte[]>(ref value), length, sizeof(long));
        }

        private static void ConvertToInt(ref int value, ReadOnlySpan<byte> input, int length)
            => value = BinaryPrimitives.ReadInt32BigEndian(input);

        private static void ConvertToIntArray(ref int[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= new int[length];

            BufferHelper.CopyAndFix(input, Unsafe.As<int[], byte[]>(ref value), length, sizeof(int));
        }

        private static void ConvertToShort(ref short value, ReadOnlySpan<byte> input, int length)
            => value = BinaryPrimitives.ReadInt16BigEndian(input);

        private static void ConvertToShortArray(ref short[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= new short[length];

            BufferHelper.CopyAndFix(input, Unsafe.As<short[], byte[]>(ref value), length, sizeof(short));
        }

        private static void ConvertToByte(ref byte value, ReadOnlySpan<byte> input, int length)
            => value = input[0];

        private static void ConvertToByteArray(ref byte[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= new byte[length];

            BufferHelper.CopyAndFix(input, value, length, sizeof(byte));
        }

        private static void ConvertToBoolArray(ref bool[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= new bool[length];
            input = input.Slice(0, (length + 7) >> 3);      // (length + 7) / 8

            int valueIdx = 0;

            foreach (byte b in input)
            {
                for (int i = 0; i < 8; ++i)
                {
                    if ((uint)valueIdx >= (uint)value.Length)
                    {
                        return;
                    }

                    value[valueIdx++] = (b & (1 << i)) != 0;
                }
            }
        }

        private static void ConvertToString(ref string? value, ReadOnlySpan<byte> input, int length)
        {
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            value = Encoding.ASCII.GetString(input.Slice(2, input[1]));
#else
            value = Encoding.ASCII.GetString(input.Slice(2, input[1]).ToArray());
#endif
        }
    }
}