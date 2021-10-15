using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sally7.ValueConversion
{
    internal static class FromS7Conversions
    {
        public static Delegate GetConverter<TValue>()
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
                        sizeof(long) => new ConvertFromS7<long[]>(ConvertToLongArray<TValue>),
                        sizeof(int) => new ConvertFromS7<int[]>(ConvertToIntArray<TValue>),
                        sizeof(short) => new ConvertFromS7<short[]>(ConvertToShortArray<TValue>),
                        sizeof(byte) => new ConvertFromS7<byte[]>(ConvertToByteArray<TValue>),
                        _ => throw new NotImplementedException(),
                    };
                }
            }

            if (typeof(TValue) == typeof(string)) return new ConvertFromS7<string>(ConvertToString);

            throw new NotImplementedException();
        }

        private static void ConvertToLong(ref long value, ReadOnlySpan<byte> input, int length)
            => value = BinaryPrimitives.ReadInt64BigEndian(input);

        private static void ConvertToLongArray<TTarget>(ref long[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= Unsafe.As<long[]>(Array.CreateInstance(typeof(TTarget).GetElementType()!, length));

            for (var i = 0; i < input.Length / sizeof(long); i++)
                ConvertToLong(ref value![i], input.Slice(i * sizeof(long)), 1);
        }

        private static void ConvertToInt(ref int value, ReadOnlySpan<byte> input, int length)
            => value = BinaryPrimitives.ReadInt32BigEndian(input);

        private static void ConvertToIntArray<TTarget>(ref int[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= Unsafe.As<int[]>(Array.CreateInstance(typeof(TTarget).GetElementType()!, length));

            for (var i = 0; i < input.Length / sizeof(int); i++)
                ConvertToInt(ref value![i], input.Slice(i * sizeof(int)), 1);
        }

        private static void ConvertToShort(ref short value, ReadOnlySpan<byte> input, int length)
            => value = BinaryPrimitives.ReadInt16BigEndian(input);

        private static void ConvertToShortArray<TTarget>(ref short[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= Unsafe.As<short[]>(Array.CreateInstance(typeof(TTarget).GetElementType()!, length));

            for (var i = 0; i < input.Length / sizeof(short); i++)
                ConvertToShort(ref value![i], input.Slice(i * sizeof(short)), 1);
        }

        private static void ConvertToByte(ref byte value, ReadOnlySpan<byte> input, int length)
            => value = input[0];

        private static void ConvertToByteArray<TTarget>(ref byte[]? value, ReadOnlySpan<byte> input, int length)
        {
            value ??= Unsafe.As<byte[]>(Array.CreateInstance(typeof(TTarget).GetElementType()!, length));

            input.CopyTo(value);
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