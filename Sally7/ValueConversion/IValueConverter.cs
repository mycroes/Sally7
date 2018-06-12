using System;

namespace Sally7.ValueConversion
{
    public interface IValueConverter
    {
        int EncodeDataItemValue(in IDataItem dataItem, Span<byte> buffer);
        void DecodeDataItemValue(in Span<byte> buffer, IDataItem dataItem, int length);
    }
}