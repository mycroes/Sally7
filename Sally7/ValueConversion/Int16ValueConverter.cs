using System;

namespace Sally7.ValueConversion
{
    internal class Int16ValueConverter : IValueConverter
    {
        public int EncodeDataItemValue(in IDataItem dataItem, Span<byte> buffer)
        {
            var value = ((IDataItem<short>) dataItem).Value;

            buffer[0] = (byte) (value >> 8);
            buffer[1] = (byte) value;

            return 2;
        }

        public void DecodeDataItemValue(in Span<byte> buffer, IDataItem dataItem, int length)
        {
            if (length != 2) throw new ArgumentException($"Received length {length} while the size of an Int16 is 2.");

            ((IDataItem<short>) dataItem).Value = (short) (buffer[0] << 8 | buffer[1]);
        }

        public int GetDataItemLength(in IDataItem dataItem)
        {
            return 2 << 3;
        }
    }
}