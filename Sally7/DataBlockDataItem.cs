using System;
using Sally7.Protocol;
using Sally7.Protocol.S7;

namespace Sally7
{
    public class DataBlockDataItem<TValue> : IDataItem<TValue>
    {
        private Address address;
        private int startByte;
        public BigEndianShort DbNumber { get; set; }
        public int Length { get; set; }
        public TValue Value { get; set; }

        public int StartByte
        {
            get => startByte;
            set => address.FromStartByteAndBit(startByte = value, 0);
        }

        Address IDataItem.Address => address;
        Area IDataItem.Area => Area.DataBlock;
        TransportSize IDataItem.TransportSize => TransportSize.Byte;
        Type IDataItem.ValueType => typeof(TValue);
        VariableType IDataItem.VariableType => VariableType.Byte;
    }
}