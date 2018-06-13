using System;
using System.Collections.Generic;

namespace Sally7.ValueConversion
{
    internal class DelegatingValueConverter : IValueConverter
    {
        private readonly Dictionary<Type, IValueConverter> valueConverters = BuildValueConverters();

        public int EncodeDataItemValue(in IDataItem dataItem, Span<byte> buffer)
        {
            return ValueConverter(dataItem.ValueType).EncodeDataItemValue(dataItem, buffer);
        }

        public void DecodeDataItemValue(in Span<byte> buffer, IDataItem dataItem, int length)
        {
            ValueConverter(dataItem.ValueType).DecodeDataItemValue(buffer, dataItem, length);
        }

        public int GetDataItemLength(in IDataItem dataItem)
        {
            return ValueConverter(dataItem.ValueType).GetDataItemLength(dataItem);
        }

        private static Dictionary<Type, IValueConverter> BuildValueConverters()
        {
            return new Dictionary<Type, IValueConverter>
            {
                {typeof(bool), new BoolValueConverter()},
                {typeof(int), new Int32ValueConverter()},
                {typeof(short), new Int16ValueConverter()}
            };
        }

        private IValueConverter ValueConverter(Type type)
        {
            if (valueConverters.TryGetValue(type, out var converter)) return converter;

            throw new ArgumentException($"No converter is known for type {type.FullName}.");
        }
    }
}
