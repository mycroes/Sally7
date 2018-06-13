using System;

namespace Sally7.ValueConversion
{
    internal class BoolValueConverter : IValueConverter
    {
        public int GetDataItemLength(in IDataItem dataItem)
        {
            return 1;
        }

        public int EncodeDataItemValue(in IDataItem dataItem, Span<byte> buffer)
        {
            buffer[0] = ((IDataItem<bool>) dataItem).Value ? (byte) 1 : (byte) 0;
            buffer[1] = 0;

            return 2;
        }

        public void DecodeDataItemValue(in Span<byte> buffer, IDataItem dataItem, int length)
        {
            if (length != 1) throw new ArgumentException($"Received length {length} while the size of a Boolean is 1.");

            ((IDataItem<bool>) dataItem).Value = buffer[0] == 1;
        }
    }
}