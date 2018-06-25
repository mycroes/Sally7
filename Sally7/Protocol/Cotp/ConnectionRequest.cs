using System.Runtime.InteropServices;

namespace Sally7.Protocol.Cotp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ConnectionRequest
    {
        public byte ConnectionRequestAndCredit;
        public BigEndianShort DestinationReference;
        public BigEndianShort SourceReference;
        public byte ClassAndOption;

        public void Init()
        {
            ConnectionRequestAndCredit = 0b1110_0000;
            ClassAndOption = 0; // Only class 0 is supported when using TPKT over TCP
            DestinationReference = default; // Anything
            SourceReference = default; // Anything
        }
    }
}
