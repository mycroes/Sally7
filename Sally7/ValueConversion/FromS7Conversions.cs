using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sally7.ValueConversion
{
    internal static class FromS7Conversions
    {
        public static Delegate GetConverter<TValue>()
        {
            var type = typeof(TValue);

            if (type.IsValueType)
            {
                switch (Unsafe.SizeOf<TValue>())
                {
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

            if (type.IsArray)
            {
                type = type.GetElementType() ?? throw new Exception(
                    $"Type {typeof(TValue)} doesn't have an ElementType.");

                switch (ConversionHelper.SizeOf(type))
                {
                    case sizeof(int):
                        return new ConvertFromS7<int[]>(ConvertToIntArray);
                    case sizeof(short):
                        return new ConvertFromS7<short[]>(ConvertToShortArray);
                    case sizeof(byte):
                        return new ConvertFromS7<byte[]>(ConvertToByteArray);
                    default:
                        throw new NotImplementedException();
                }
            }

            if (type == typeof(string)) return new ConvertFromS7<string>(ConvertToString);

            throw new NotImplementedException();
        }

        private static void ConvertToInt(ref int value, in Span<byte> input, in int length)
        {
            value = input[0] << 24 | input[1] << 16 | input[2] << 8 | input[3];
        }

        private static void ConvertToIntArray(ref int[] value, in Span<byte> input, in int length)
        {
            for (var i = 0; i < input.Length / sizeof(int); i++)
                ConvertToInt(ref value[i], input.Slice(i * sizeof(int)), 1);
        }

        private static void ConvertToShort(ref short value, in Span<byte> input, in int length)
        {
            value = (short) (input[0] << 8 | input[1]);
        }

        private static void ConvertToShortArray(ref short[] value, in Span<byte> input, in int length)
        {
            for (var i = 0; i < input.Length / sizeof(short); i++)
                ConvertToShort(ref value[i], input.Slice(i * sizeof(short)), 1);
        }

        private static void ConvertToByte(ref byte value, in Span<byte> input, in int length)
        {
            value = input[0];
        }

        private static void ConvertToByteArray(ref byte[] value, in Span<byte> input, in int length)
        {
            input.CopyTo(value);
        }

        private static void ConvertToString(ref string value, in Span<byte> input, in int length)
        {
            value = Encoding.ASCII.GetString(input.Slice(2, input[1]).ToArray());
        }
    }
}