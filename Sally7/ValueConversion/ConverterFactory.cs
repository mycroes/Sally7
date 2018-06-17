using System;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    public delegate int ConvertToS7<TValue>(in TValue value, in int length, in Span<byte> output);

    public delegate void ConvertFromS7<TValue>(ref TValue value, in Span<byte> input, in int length);

    internal class ConverterFactory
    {
        public static ConvertFromS7<TValue> GetFromPlcConverter<TValue>() => Unsafe.As<ConvertFromS7<TValue>>(GetFromPlcConverter(typeof(TValue)));

        private static Delegate GetFromPlcConverter(Type type)
        {
            if (type.IsEnum) type = Enum.GetUnderlyingType(type);

            if (type == typeof(int)) return new ConvertFromS7<int>(ConvertToInt);
            if (type == typeof(float)) return new ConvertFromS7<int>(ConvertToInt);

            throw new NotImplementedException();
        }

        private static void ConvertToInt(ref int value, in Span<byte> input, in int length)
        {
            value = input[0] << 24 | input[1] << 16 | input[2] << 8 | input[3];
        }
    }
}
