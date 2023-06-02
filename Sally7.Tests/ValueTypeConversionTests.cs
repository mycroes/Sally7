using Sally7.ValueConversion;

namespace Sally7.Tests
{
    public class ValueTypeConversionTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData((long) short.MinValue)]
        [InlineData((long) short.MaxValue)]
        [InlineData((long) int.MinValue)]
        [InlineData((long) int.MaxValue)]
        [InlineData(long.MinValue)]
        [InlineData(long.MaxValue)]
        public void ConvertToLong(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            var converter = ConverterFactory.GetFromPlcConverter<long>();
            long result = default;
            converter(ref result, bytes, sizeof(long));

            Assert.Equal(value, result);
        }

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
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(short.MinValue)]
        [InlineData(short.MaxValue)]
        public void ConvertToShort(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            var converter = ConverterFactory.GetFromPlcConverter<short>();
            short result = default;
            converter(ref result, bytes, sizeof(short));

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

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(100)]
        [InlineData(127)]
        [InlineData(128)]
        [InlineData(129)]
        public void ConvertToBoolArray_Roundtrips(int boolArraySize)
        {
            var bools = new bool[boolArraySize];
            var bytes = new byte[boolArraySize];
            var random = new Random();

            for (int i = 0; i < bools.Length; ++i)
            {
                bools[i] = random.Next() % 2 == 0;
            }

            var toS7Converter = ConverterFactory.GetToPlcConverter<bool[]>();
            var fromS7Converter = ConverterFactory.GetFromPlcConverter<bool[]>();

            int length = toS7Converter(bools, bools.Length, bytes);

            bool[]? actual = null;
            fromS7Converter(ref actual, bytes, boolArraySize);

            Assert.Equal(boolArraySize, actual!.Length);

            for (int i = 0; i < boolArraySize; ++i)
            {
                Assert.Equal(bools[i], actual[i]);
            }
        }
    }
}