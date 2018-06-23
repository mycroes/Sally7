using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    public delegate int ConvertToS7<TValue>(in TValue value, in int length, in Span<byte> output);

    public delegate void ConvertFromS7<TValue>(ref TValue value, in Span<byte> input, in int length);

    internal static class ConverterFactory
    {
        private static readonly MethodInfo sizeOfMethod = typeof(Unsafe).GetMethod(nameof(Unsafe.SizeOf));

        public static ConvertFromS7<TValue> GetFromPlcConverter<TValue>()
        {
            if (typeof(TValue).IsValueType)
                return Unsafe.As<ConvertFromS7<TValue>>(GetFromPlcConverter(typeof(TValue), Unsafe.SizeOf<TValue>()));

            if (typeof(TValue).IsArray)
            {
                var elementType = typeof(TValue).GetElementType() ?? throw new Exception($"Type {typeof(TValue)} doesn't have an ElementType.");
                return Unsafe.As<ConvertFromS7<TValue>>(GetFromPlcArrayConverter(elementType,
                    (int) sizeOfMethod.MakeGenericMethod(elementType).Invoke(null, new object[0])));
            }

            if (typeof(TValue) == typeof(string))
                throw new NotImplementedException();

            throw new ArgumentException();
        }

        private static Delegate GetFromPlcArrayConverter(Type type, int elementSize)
        {
            if (type.IsValueType)
            {
                switch (elementSize)
                {
                    case sizeof(int):
                        return new ConvertFromS7<int[]>(ConvertToIntArray);
                    case sizeof(short):
                        return new ConvertFromS7<short[]>(ConvertToShortArray);
                    case sizeof(byte):
                        return new ConvertFromS7<byte[]>(ConvertToByteArray);
                }
            }

            throw new NotImplementedException();
        }

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
            for (var i = 0; i < input.Length; i++)
                value[i] = input[i];
        }
    }
}
