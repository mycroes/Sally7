namespace Sally7.Protocol.S7
{
    internal enum AddressingMode : byte
    {
        S7Any = 0x10, // S7-Any pointer (regular addressing) memory+variable length+offset
        DriveES = 0xa2, // Drive-ES-Any seen on Drive ES Starter with routing over S7
        SubItem = 0xb0, // Special DB addressing for S400 (subitem read/write)
        Symbolic = 0xb2 // S1200/S1500? Symbolic addressing mode
    }
}