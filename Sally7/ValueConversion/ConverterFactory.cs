using System;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    public delegate int ConvertToS7<TValue>(in TValue value, in int length, in Span<byte> output);

    public delegate void ConvertFromS7<TValue>(ref TValue value, in Span<byte> input, in int length);

    internal static class ConverterFactory
    {
        public static ConvertFromS7<TValue> GetFromPlcConverter<TValue>() =>
            Unsafe.As<ConvertFromS7<TValue>>(FromS7Conversions.GetConverter<TValue>());

        public static ConvertToS7<TValue> GetToPlcConverter<TValue>() =>
            Unsafe.As<ConvertToS7<TValue>>(ToS7Conversions.GetConverter<TValue>());
    }
}
