using System;
using System.Runtime.InteropServices;

namespace Sally7.ValueConversion
{
    internal class FloatValueConverter : IValueConverter
    {
        public int GetDataItemLength(in IDataItem dataItem)
        {
            return 4;
        }

        public int EncodeDataItemValue(in IDataItem dataItem, Span<byte> buffer)
        {
            var value = IntFloatUnion.ToInt(((IDataItem<float>) dataItem).Value);

            buffer[0] = (byte) (value >> 24);
            buffer[1] = (byte) (value >> 16);
            buffer[2] = (byte) (value >> 8);
            buffer[3] = (byte) value;

            return 4;
        }

        public void DecodeDataItemValue(in Span<byte> buffer, IDataItem dataItem, int length)
        {
            if (length != 4) throw new ArgumentException($"Received length {length} while the size of a float is 4.");

            ((IDataItem<float>) dataItem).Value =
                IntFloatUnion.ToFloat(buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3]);
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct IntFloatUnion
        {
            [FieldOffset(0)]
            private int intValue;

            [FieldOffset(0)]
            private float floatValue;

            public static float ToFloat(in int intValue)
            {
                return new IntFloatUnion {floatValue = 0, intValue = intValue}.floatValue;
            }

            public static int ToInt(in float floatValue)
            {
                return new IntFloatUnion {intValue = 0, floatValue = floatValue}.intValue;
            }
        }
    }
}