using System.Runtime.InteropServices;

namespace Sally7.Protocol.S7
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RequestItem
    {
        public byte Spec;
        public byte Length;
        public AddressingMode SyntaxId;
        public VariableType VariableType;
        public BigEndianShort Count;
        public BigEndianShort DbNumber;
        public Area Area;
        public Address Address;

        public void Init()
        {
            Spec = 0x12;
            Length = 10;
            SyntaxId = AddressingMode.S7Any;
        }
    }
}