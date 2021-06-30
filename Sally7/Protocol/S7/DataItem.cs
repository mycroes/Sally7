using System.Runtime.InteropServices;

namespace Sally7.Protocol.S7
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DataItem
    {
        /// <summary>
        /// The size of the <see cref="DataItem"/> struct.
        /// </summary>
        public const int Size =
            sizeof(ReadWriteErrorCode) + // ErrorCode
            sizeof(TransportSize) + // TransportSize
            BigEndianShort.Size; // Count

        public ReadWriteErrorCode ErrorCode;
        public TransportSize TransportSize;
        public BigEndianShort Count;
        // byte[Count] Data
    }
}