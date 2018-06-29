using System;
using Sally7.Protocol;
using Sally7.Protocol.S7;
using Sally7.ValueConversion;

namespace Sally7
{
    public class DataBlockDataItem<TValue> : IDataItem<TValue>
    {
        private readonly VariableType variableType;
        private readonly TransportSize transportSize;
        private readonly int elementSize;
        private readonly ConvertToS7<TValue> toS7Converter;
        private readonly ConvertFromS7<TValue> fromS7Converter;

        public DataBlockDataItem() : this(ConverterFactory.GetToPlcConverter<TValue>(),
            ConverterFactory.GetFromPlcConverter<TValue>(), ConversionHelper.GetElementSize<TValue>())
        {
            if (typeof(TValue) == typeof(bool))
            {
                variableType = VariableType.Bit;
                transportSize = TransportSize.Bit;
            }
            else
            {
                variableType = VariableType.Byte;
                transportSize = TransportSize.Byte;
            }
        }

        private DataBlockDataItem(ConvertToS7<TValue> toS7Converter, ConvertFromS7<TValue> fromS7Converter, int elementSize)
        {
            this.toS7Converter = toS7Converter;
            this.fromS7Converter = fromS7Converter;
            this.elementSize = elementSize;

            if (typeof(TValue).IsValueType) SetLength(1);
        }

        private Address address;
        private int startByte;
        private int bit;
        private TValue value;
        private int length;

        public BigEndianShort DbNumber { get; set; }
        public BigEndianShort ReadCount { get; private set; }

        public int Length
        {
            get => length;
            set => SetLength(value);
        }

        public TValue Value
        {
            get => value;
            set => this.value = value;
        }

        public int StartByte
        {
            get => startByte;
            set => address.FromStartByteAndBit(startByte = value, bit);
        }

        public int Bit
        {
            get => bit;
            set => address.FromStartByteAndBit(startByte, bit = value);
        }

        Address IDataItem.Address => address;
        Area IDataItem.Area => Area.DataBlock;
        TransportSize IDataItem.TransportSize => transportSize;
        VariableType IDataItem.VariableType => variableType;

        int IDataItem.WriteValue(in Span<byte> output) => toS7Converter.Invoke(value, Length, output);

        void IDataItem.ReadValue(in ReadOnlySpan<byte> input) => fromS7Converter.Invoke(ref value, input, Length);

        private void SetLength(in int newLength)
        {
            if (length == newLength) return;

            length = newLength;
            if (typeof(TValue) == typeof(string))
            {
                ReadCount = length + 2;
            }
            else
            {
                ReadCount = length * elementSize;
            }
        }
    }
}