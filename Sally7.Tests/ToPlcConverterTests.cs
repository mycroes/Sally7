using System;
using Sally7.ValueConversion;
using Xunit;

namespace Sally7.Tests;

public static class ToPlcConverterTests
{
    public class Elements
    {
        [Theory]
        [TestValuesData]
        public void ConvertByteToPlc(byte value)
        {
            // Arrange
            var converter = ConverterFactory.GetToPlcConverter<byte>(1);
            var buffer = new byte[sizeof(byte)];

            // Act
            converter(value, 1, buffer);

            // Assert
            Assert.Equal([value], buffer);
        }

        [Theory]
        [TestValuesData]
        public void ConvertSByteToPlc(sbyte value)
        {
            // Arrange
            var converter = ConverterFactory.GetToPlcConverter<sbyte>(1);
            var buffer = new byte[sizeof(sbyte)];

            // Act
            converter(value, 1, buffer);

            // Assert
            Assert.Equal([(byte)value], buffer);
        }

        [Theory]
        [TestValuesData]
        public void ConvertInt16ToPlc(short value)
        {
            TestConvertToPlc(value, sizeof(short), BitConverter.GetBytes);
        }

        [Theory]
        [TestValuesData]
        public void ConvertUInt16ToPlc(ushort value)
        {
            TestConvertToPlc(value, sizeof(ushort), BitConverter.GetBytes);
        }

        [Theory]
        [TestValuesData]
        public void ConvertInt32ToPlc(int value)
        {
            TestConvertToPlc(value, sizeof(int), BitConverter.GetBytes);
        }

        [Theory]
        [TestValuesData]
        public void ConvertUInt32ToPlc(uint value)
        {
            TestConvertToPlc(value, sizeof(uint), BitConverter.GetBytes);
        }

        [Theory]
        [TestValuesData]
        public void ConvertSingleToPlc(float value)
        {
            TestConvertToPlc(value, sizeof(float), BitConverter.GetBytes);
        }

        [Theory]
        [TestValuesData]
        public void ConvertInt64ToPlc(long value)
        {
            TestConvertToPlc(value, sizeof(long), BitConverter.GetBytes);
        }

        [Theory]
        [TestValuesData]
        public void ConvertUInt64ToPlc(ulong value)
        {
            TestConvertToPlc(value, sizeof(ulong), BitConverter.GetBytes);
        }

        [Theory]
        [TestValuesData]
        public void ConvertDoubleToPlc(double value)
        {
            TestConvertToPlc(value, sizeof(double), BitConverter.GetBytes);
        }

        private static void TestConvertToPlc<T>(T value, int size, Func<T, byte[]> getBytes)
        {
            // Arrange
            var converter = ConverterFactory.GetToPlcConverter<T>(1);
            var buffer = new byte[size];

            // Act
            converter(value, 1, buffer);

            // Assert
            var bytes = getBytes.Invoke(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            Assert.Equal(bytes, buffer);
        }
    }

    public class Arrays
    {
        [Theory]
        [TestValuesData]
        public void ConvertFloatArrToPlc(float value)
        {
            // Arrange
            var converter = ConverterFactory.GetToPlcConverter<float[]>(1);
            var buffer = new byte[sizeof(float)];

            // Act
            converter([value], 1, buffer);

            // Assert
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            Assert.Equal(bytes, buffer);
        }
    }
}