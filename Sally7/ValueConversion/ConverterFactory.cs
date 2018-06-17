using System;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    public delegate int ConvertToS7<TValue>(in TValue value, in int length, in Span<byte> output);

    public delegate TValue ConvertFromS7<out TValue>(in Span<byte> input, in int length);

    internal class ConverterFactory
    {
        public static ConvertFromS7<TValue> GetFromPlcConverter<TValue>() => Unsafe.As<ConvertFromS7<TValue>>(GetFromPlcConverter(typeof(TValue)));

        private static Delegate GetFromPlcConverter(Type type)
        {
            if (type.IsEnum) type = Enum.GetUnderlyingType(type);

            if (type == typeof(int)) return new ConvertFromS7<int>(ConvertToInt);
            if (type == typeof(float)) return new ConvertFromS7<float>(ConvertToIntSized<float>);

            throw new NotImplementedException();
        }

        private static int ConvertToInt(in Span<byte> input, in int length)
        {
            return input[0] << 24 | input[1] << 16 | input[2] << 8 | input[3];
        }

        private static TValue ConvertToIntSized<TValue>(in Span<byte> input, in int length)
        {
            var value = ConvertToInt(input, length);
            return Unsafe.As<int, TValue>(ref value);
        }
    }
}
