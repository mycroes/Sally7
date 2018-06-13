using System;

namespace Sally7.ValueConversion
{
    internal class Int32ValueConverter : IValueConverter
    {
        public int EncodeDataItemValue(in IDataItem dataItem, Span<byte> buffer)
        {
            var value = ((IDataItem<int>) dataItem).Value;

            buffer[0] = (byte) (value >> 24);
            buffer[1] = (byte) (value >> 16);
            buffer[2] = (byte) (value >> 8);
            buffer[3] = (byte) value;

            return 4;
        }

        public void DecodeDataItemValue(in Span<byte> buffer, IDataItem dataItem, int length)
        {
            if (length != 4) throw new ArgumentException($"Received length {length} while the size of an Int32 is 4.");

            ((IDataItem<int>) dataItem).Value = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];
        }

        public int GetDataItemLength(in IDataItem dataItem)
        {
            return 4 << 3;
        }
    }
}