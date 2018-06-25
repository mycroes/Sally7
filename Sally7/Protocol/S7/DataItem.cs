using System.Runtime.InteropServices;

namespace Sally7.Protocol.S7
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DataItem
    {
        public ReadWriteErrorCode ErrorCode;
        public TransportSize TransportSize;
        public BigEndianShort Count;
        // byte[Count] Data
    }
}