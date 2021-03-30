using System;
using System.Linq;
using Sally7.ValueConversion;
using Xunit;

namespace Sally7.Tests
{
    public class ArrayConversionTests
    {
        [Theory]
        [InlineData(new int[] {0})]
        [InlineData(new int[] {1, 1 << 8, 1 << 16, 1 << 24})]
        public void ConvertToIntArray(int[] value)
        {
            var bytes = value.SelectMany(v =>
            {
                var b = BitConverter.GetBytes(v);
                if (BitConverter.IsLittleEndian) Array.Reverse(b);
                return b;
            }).ToArray();

            var converter = ConverterFactory.GetFromPlcConverter<int[]>();
            var result = new int[value.Length];
            converter(ref result, bytes, value.Length);

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(new short[] {0})]
        [InlineData(new short[] {1, 1 << 8})]
        public void ConvertToShortArray(short[] value)
        {
            var bytes = value.SelectMany(v =>
            {
                var b = BitConverter.GetBytes(v);
                if (BitConverter.IsLittleEndian) Array.Reverse(b);
                return b;
            }).ToArray();

            var converter = ConverterFactory.GetFromPlcConverter<short[]>();
            var result = new short[value.Length];
            converter(ref result, bytes, value.Length);

            Assert.Equal(value, result);
        }
    }
}
