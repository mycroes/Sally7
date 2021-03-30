using System;
using Sally7.Protocol;
using Sally7.Protocol.S7;

namespace Sally7
{
    public interface IDataItem
    {
        Area Area { get; }
        BigEndianShort DbNumber { get; }
        Address Address { get; }
        BigEndianShort ReadCount { get; }
        
        TransportSize TransportSize { get; }
        VariableType VariableType { get; }

        int WriteValue(in Span<byte> output);
        void ReadValue(in ReadOnlySpan<byte> input);
    }

    public interface IDataItem<TValue> : IDataItem
    {
        TValue? Value { get; set; }
    }
}