using System.Runtime.InteropServices;

namespace Sally7.Protocol.S7
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct RequestItem
    {
        /// <summary>
        /// The size of the <see cref="RequestItem"/> struct.
        /// </summary>
        public const int Size =
            sizeof(byte) + // Spec
            sizeof(byte) + // Length
            sizeof(AddressingMode) + // SyntaxId
            sizeof(VariableType) + // VariableType
            BigEndianShort.Size + // Count
            BigEndianShort.Size + // DbNumber
            sizeof(Area) + // Area
            Address.Size; // Address

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