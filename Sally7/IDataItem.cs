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
        int Length { get; }
        TransportSize TransportSize { get; }
        VariableType VariableType { get; }
        Type ValueType { get; }
    }

    public interface IDataItem<TValue> : IDataItem
    {
        TValue Value { get; set; }
    }
}