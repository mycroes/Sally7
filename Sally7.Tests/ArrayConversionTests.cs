using Sally7.ValueConversion;

namespace Sally7.Tests
{
    public class ArrayConversionTests
    {
        [Theory]
        [InlineData(new short[] { 1 })]
        [InlineData(new short[] { 1, 1 << 8 })]
        public void ConvertToShortArray(short[] value)
        {
            var bytes = value.SelectMany(v =>
            {
                var b = BitConverter.GetBytes(v);
                if (BitConverter.IsLittleEndian) Array.Reverse(b);
                return b;
            }).ToArray();

            var converter = ConverterFactory.GetFromPlcConverter<short[]>(value.Length);
            var result = new short[value.Length];
            converter(ref result, bytes, value.Length);

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 1 << 8, 1 << 16, 1 << 24 })]
        public void ConvertToIntArray(int[] value)
        {
            var bytes = value.SelectMany(v =>
            {
                var b = BitConverter.GetBytes(v);
                if (BitConverter.IsLittleEndian) Array.Reverse(b);
                return b;
            }).ToArray();

            var converter = ConverterFactory.GetFromPlcConverter<int[]>(value.Length);
            var result = new int[value.Length];
            converter(ref result, bytes, value.Length);

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(new long[] { 1 })]
        [InlineData(new long[] { 1, 1 << 8, 1 << 16, 1 << 24, 1 << 32, 1 << 40, 1 << 48, 1 << 56 })]
        public void ConvertToLongArray(long[] value)
        {
            var bytes = value.SelectMany(v =>
            {
                var b = BitConverter.GetBytes(v);
                if (BitConverter.IsLittleEndian) Array.Reverse(b);
                return b;
            }).ToArray();

            var converter = ConverterFactory.GetFromPlcConverter<long[]>(value.Length);
            var result = new long[value.Length];
            converter(ref result, bytes, value.Length);

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(new float[] { 3.14f })]
        [InlineData(new float[] { 2.81f, 3.14f })]
        public void ConvertToFloatArray(float[] value)
        {
            var bytes = value.SelectMany(v =>
            {
                var b = BitConverter.GetBytes(v);
                if (BitConverter.IsLittleEndian) Array.Reverse(b);
                return b;
            }).ToArray();

            var converter = ConverterFactory.GetFromPlcConverter<float[]>(value.Length);
            var result = new float[value.Length];
            converter(ref result, bytes, value.Length);

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(new double[] { 3.14 })]
        [InlineData(new double[] { 2.81, 3.14 })]
        public void ConvertToDoubleArray(double[] value)
        {
            var bytes = value.SelectMany(v =>
            {
                var b = BitConverter.GetBytes(v);
                if (BitConverter.IsLittleEndian) Array.Reverse(b);
                return b;
            }).ToArray();

            var converter = ConverterFactory.GetFromPlcConverter<double[]>(value.Length);
            var result = new double[value.Length];
            converter(ref result, bytes, value.Length);

            Assert.Equal(value, result);
        }
    }
}
