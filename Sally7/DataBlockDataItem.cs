using System;
using Sally7.Internal;
using Sally7.Protocol;
using Sally7.Protocol.S7;
using Sally7.ValueConversion;

namespace Sally7
{
    public class DataBlockDataItem<TValue> : IDataItem<TValue>
    {
        private readonly ConvertToS7<TValue> _toS7Converter;
        private readonly ConvertFromS7<TValue> _fromS7Converter;
        private TValue? _value;

        public DataBlockDataItem(BigEndianShort dbNumber, int startByte, int length = 1) : this(dbNumber, startByte, 0,
            length)
        {
        }

        public DataBlockDataItem(BigEndianShort dbNumber, int startByte, int bit, int length = 1)
        {
            Assertions.AssertDataItemLengthIsValidForType(length, typeof(TValue));
            Assertions.AssertBitIsValidForType(bit, typeof(TValue));

            _toS7Converter = ConverterFactory.GetToPlcConverter<TValue>(length);
            _fromS7Converter = ConverterFactory.GetFromPlcConverter<TValue>(length);
            var elementSize = ConversionHelper.GetElementSize<TValue>();

            DbNumber = dbNumber;
            StartByte = startByte;
            Bit = bit;
            Length = length;

            if (typeof(TValue) == typeof(bool))
            {
                VariableType = VariableType.Bit;
                TransportSize = TransportSize.Bit;
            }
            else
            {
                VariableType = VariableType.Byte;
                TransportSize = TransportSize.Byte;
            }

            Address = Address.FromStartByteAndBit(startByte, bit);

            if (typeof(TValue) == typeof(string))
            {
                ReadCount = length + 2;
            }
            else if (typeof(TValue) == typeof(bool[]))
            {
                ReadCount = (length + 7) >> 3; // Round to bytes
            }
            else
            {
                ReadCount = length * elementSize;
            }
        }

        public BigEndianShort DbNumber { get; }
        public int StartByte { get; }
        public int Bit { get; }
        public int Length { get; }

        public TValue? Value
        {
            get => _value;
            set => this._value = value;
        }

        public Address Address { get; }
        public BigEndianShort ReadCount { get; }
        public Area Area => Area.DataBlock;
        public TransportSize TransportSize { get; }
        public VariableType VariableType { get; }

        int IDataItem.WriteValue(Span<byte> output) => _toS7Converter.Invoke(Value, Length, output);

        void IDataItem.ReadValue(ReadOnlySpan<byte> input) => _fromS7Converter.Invoke(ref _value, input, Length);
    }
}