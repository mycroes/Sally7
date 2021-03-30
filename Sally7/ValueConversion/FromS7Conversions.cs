using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sally7.ValueConversion
{
    internal static class FromS7Conversions
    {
        public static Delegate GetConverter<TValue>()
        {
            var type = typeof(TValue);

            if (type.IsPrimitive || type.IsEnum)
            {
                switch (Unsafe.SizeOf<TValue>())
                {
                    case sizeof(long):
                        return new ConvertFromS7<long>(ConvertToLong);
                    case sizeof(int):
                        return new ConvertFromS7<int>(ConvertToInt);
                    case sizeof(short):
                        return new ConvertFromS7<short>(ConvertToShort);
                    case sizeof(byte):
                        return new ConvertFromS7<byte>(ConvertToByte);
                    default:
                        throw new NotImplementedException();
                }
            }

            if (type == typeof(bool[]))
                return new ConvertFromS7<bool[]>(ConvertToBoolArray);

            if (type.IsArray)
            {
                var elementType = type.GetElementType() ?? throw new Exception(
                    $"Type {typeof(TValue)} doesn't have an ElementType.");

                if (elementType.IsPrimitive || elementType.IsEnum)
                {
                    switch (ConversionHelper.SizeOf(elementType))
                    {
                        case sizeof(long):
                            return new ConvertFromS7<long[]>(ConvertToLongArray<TValue>);
                        case sizeof(int):
                            return new ConvertFromS7<int[]>(ConvertToIntArray<TValue>);
                        case sizeof(short):
                            return new ConvertFromS7<short[]>(ConvertToShortArray<TValue>);
                        case sizeof(byte):
                            return new ConvertFromS7<byte[]>(ConvertToByteArray<TValue>);
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            if (type == typeof(string)) return new ConvertFromS7<string>(ConvertToString);

            throw new NotImplementedException();
        }

        private static void ConvertToLong(ref long value, in ReadOnlySpan<byte> input, in int length)
        {
            value = (long) (uint) (input[0] << 24 | input[1] << 16 | input[2] << 8 | input[3]) << 32 |
                (uint) (input[4] << 24 | input[5] << 16 | input[6] << 8 | input[7]);
        }

        private static void ConvertToLongArray<TTarget>(ref long[]? value, in ReadOnlySpan<byte> input, in int length)
        {
            value ??= Unsafe.As<long[]>(Array.CreateInstance(typeof(TTarget).GetElementType(), length));

            for (var i = 0; i < input.Length / sizeof(long); i++)
                ConvertToLong(ref value![i], input.Slice(i * sizeof(long)), 1);
        }

        private static void ConvertToInt(ref int value, in ReadOnlySpan<byte> input, in int length)
        {
            value = input[0] << 24 | input[1] << 16 | input[2] << 8 | input[3];
        }

        private static void ConvertToIntArray<TTarget>(ref int[]? value, in ReadOnlySpan<byte> input, in int length)
        {
            value ??= Unsafe.As<int[]>(Array.CreateInstance(typeof(TTarget).GetElementType(), length));

            for (var i = 0; i < input.Length / sizeof(int); i++)
                ConvertToInt(ref value![i], input.Slice(i * sizeof(int)), 1);
        }

        private static void ConvertToShort(ref short value, in ReadOnlySpan<byte> input, in int length)
        {
            value = (short) (input[0] << 8 | input[1]);
        }

        private static void ConvertToShortArray<TTarget>(ref short[]? value, in ReadOnlySpan<byte> input, in int length)
        {
            value ??= Unsafe.As<short[]>(Array.CreateInstance(typeof(TTarget).GetElementType(), length));

            for (var i = 0; i < input.Length / sizeof(short); i++)
                ConvertToShort(ref value![i], input.Slice(i * sizeof(short)), 1);
        }

        private static void ConvertToByte(ref byte value, in ReadOnlySpan<byte> input, in int length)
        {
            value = input[0];
        }

        private static void ConvertToByteArray<TTarget>(ref byte[]? value, in ReadOnlySpan<byte> input, in int length)
        {
            value ??= Unsafe.As<byte[]>(Array.CreateInstance(typeof(TTarget).GetElementType(), length));

            input.CopyTo(value);
        }

        private static void ConvertToBoolArray(ref bool[]? value, in ReadOnlySpan<byte> input, in int length)
        {
            value = new BitArray(input.Slice(0, (length + 7) / 8).ToArray()).Cast<bool>().Take(length).ToArray();
        }

        private static void ConvertToString(ref string? value, in ReadOnlySpan<byte> input, in int length)
        {
            value = Encoding.ASCII.GetString(input.Slice(2, input[1]).ToArray());
        }
    }
}