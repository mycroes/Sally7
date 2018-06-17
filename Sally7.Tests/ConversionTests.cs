using System;
using Sally7.ValueConversion;
using Xunit;

namespace Sally7.Tests
{
    public class ConversionTests
    {
        private readonly ConverterFactory factory = new ConverterFactory();

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData((int) short.MinValue)]
        [InlineData((int) short.MaxValue)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ConvertToInt(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            var converter = ConverterFactory.GetFromPlcConverter<int>();
            int result = default;
            converter(ref result, bytes, sizeof(int));

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(0f)]
        [InlineData(1f)]
        [InlineData(0.1f)]
        [InlineData(float.MinValue)]
        [InlineData(float.MaxValue)]
        public void ConvertToFloat(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            var converter = ConverterFactory.GetFromPlcConverter<float>();
            float result = default;
            converter(ref result, bytes, sizeof(float));

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(EnumOfInt.Zero)]
        [InlineData(EnumOfInt.One)]
        [InlineData(EnumOfInt.Two)]
        [InlineData(EnumOfInt.Three)]
        [InlineData(EnumOfInt.Four)]
        [InlineData(EnumOfInt.Five)]
        [InlineData(EnumOfInt.Six)]
        [InlineData(EnumOfInt.Seven)]
        [InlineData(EnumOfInt.Eight)]
        [InlineData(EnumOfInt.Nine)]
        [InlineData(EnumOfInt.Ten)]
        public void ConvertToEnumOfInt(EnumOfInt value)
        {
            var bytes = BitConverter.GetBytes((int) value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            var converter = ConverterFactory.GetFromPlcConverter<EnumOfInt>();
            EnumOfInt result = default;
            converter(ref result, bytes, sizeof(int));

            Assert.Equal(value, result);
        }

        public enum EnumOfInt
        {
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten
        }
    }
}
