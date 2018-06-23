using System;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    public delegate int ConvertToS7<TValue>(in TValue value, in int length, in Span<byte> output);

    public delegate void ConvertFromS7<TValue>(ref TValue value, in Span<byte> input, in int length);

    internal static class ConverterFactory
    {
        public static ConvertFromS7<TValue> GetFromPlcConverter<TValue>() =>
            Unsafe.As<ConvertFromS7<TValue>>(GetFromPlcConverter(typeof(TValue), Unsafe.SizeOf<TValue>()));

        private static Delegate GetFromPlcConverter(Type type, int size)
        {
            if (type.IsValueType)
            {
                switch (size)
                {
                    case sizeof(int):
                        return new ConvertFromS7<int>(ConvertToInt);
                    case sizeof(short):
                        return new ConvertFromS7<short>(ConvertToShort);
                    case sizeof(byte):
                        return new ConvertFromS7<byte>(ConvertToByte);
                }
            }

            throw new NotImplementedException();
        }

        private static void ConvertToInt(ref int value, in Span<byte> input, in int length)
        {
            value = input[0] << 24 | input[1] << 16 | input[2] << 8 | input[3];
        }

        private static void ConvertToShort(ref short value, in Span<byte> input, in int length)
        {
            value = (short) (input[0] << 8 | input[1]);
        }

        private static void ConvertToByte(ref byte value, in Span<byte> input, in int length)
        {
            value = input[0];
        }
    }
}
