using System;
using Sally7.Protocol;
using Sally7.Protocol.S7;

namespace Sally7
{
    public class DataBlockBit : IDataItem<bool>
    {
        private Address address;
        private int startByte;
        private int bit;
        public BigEndianShort DbNumber { get; set; }
        public bool Value { get; set; }

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
        int IDataItem.Length => 1;
        TransportSize IDataItem.TransportSize => TransportSize.Bit;
        Type IDataItem.ValueType => typeof(bool);
        VariableType IDataItem.VariableType => VariableType.Bit;
    }
}