using System.Runtime.InteropServices;

namespace Sally7.Protocol.Cotp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct PduSizeParameter
    {
        public ParameterCode Code;
        public byte Length;
        public PduSize Value;

        public void Init(in PduSize pduSize)
        {
            Code = ParameterCode.TpduSize;
            Value = pduSize;
            Length = 1;
        }

        public enum PduSize : byte
        {
            Pdu128 = 0b0000_0111,
            Pdu256 = 0b0000_1000,
            Pdu512 = 0b0000_1001,
            Pdu1024 = 0b0000_1010,
            Pdu2048 = 0b0000_1011,
            Pdu4096 = 0b0000_1100, // Not allowed in class 0
            Pdu8192 = 0b0000_1101 // Not allowed in class 0
        }
    }
}