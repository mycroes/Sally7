using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sally7.ValueConversion
{
    internal static class ToS7Conversions
    {
        public static Delegate GetConverter<TValue>()
        {
            var type = typeof(TValue);

            if (type.IsValueType)
            {
                switch (Unsafe.SizeOf<TValue>())
                {
                    case sizeof(int):
                        return new ConvertToS7<int>(ConvertFromInt);
                    case sizeof(short):
                        return new ConvertToS7<short>(ConvertFromShort);
                    case sizeof(byte):
                        return new ConvertToS7<byte>(ConvertFromByte);
                    default:
                        throw new NotImplementedException();
                }
            }

            if (type == typeof(bool[]))
                return new ConvertToS7<bool[]>(ConvertFromBoolArray);

            if (type.IsArray)
            {
                type = type.GetElementType() ??
                    throw new Exception($"Type {typeof(TValue)} doesn't have an ElementType.");

                switch (ConversionHelper.SizeOf(type))
                {
                    case sizeof(int):
                        return new ConvertToS7<int[]>(ConvertFromIntArray);
                    case sizeof(short):
                        return new ConvertToS7<short[]>(ConvertFromShortArray);
                    case sizeof(byte):
                        return new ConvertToS7<byte[]>(ConvertFromByteArray);
                    default:
                        throw new NotImplementedException();
                }
            }

            if (type == typeof(string)) return new ConvertToS7<string>(ConvertFromString);

            throw new NotImplementedException();
        }

        private static int ConvertFromInt(in int value, in int length, in Span<byte> output)
        {
            output[0] = (byte) (value >> 24);
            output[1] = (byte) (value >> 16);
            output[2] = (byte) (value >> 8);
            output[3] = (byte) value;

            return sizeof(int);
        }

        private static int ConvertFromIntArray(in int[] value, in int length, in Span<byte> output)
        {
            for (var i = 0; i < value.Length; i++)
                ConvertFromInt(value[i], 1, output.Slice(i * sizeof(int)));

            return value.Length * sizeof(int);
        }

        private static int ConvertFromShort(in short value, in int length, in Span<byte> output)
        {
            output[0] = (byte) (value >> 8);
            output[1] = (byte) value;

            return sizeof(short);
        }

        private static int ConvertFromShortArray(in short[] value, in int length, in Span<byte> output)
        {
            for (var i = 0; i < value.Length; i++)
                ConvertFromShort(value[i], 1, output.Slice(i * sizeof(short)));

            return value.Length * sizeof(short);
        }

        private static int ConvertFromByte(in byte value, in int length, in Span<byte> output)
        {
            output[0] = value;

            return sizeof(byte);
        }

        private static int ConvertFromByteArray(in byte[] value, in int length, in Span<byte> output)
        {
            value.AsSpan().CopyTo(output);

            return value.Length;
        }

        private static int ConvertFromBoolArray(in bool[] value, in int length, in Span<byte> output)
        {
            var bitArray = new BitArray(value);
            var byteArray = new byte[(length + 7) / 8];
            bitArray.CopyTo(byteArray, 0);
            byteArray.CopyTo(output);

            return byteArray.Length;
        }

        private static int ConvertFromString(in string value, in int length, in Span<byte> output)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            var span = bytes.AsSpan();
            if (span.Length > length - 2) span = span.Slice(0, length - 2);

            output[0] = (byte) length;
            output[1] = (byte) span.Length;
            span.CopyTo(output.Slice(2));

            return span.Length + 2;
        }
    }
}