namespace Sally7.Protocol.S7
{
    public enum TransportSize : byte
    {
        None = 0x00,
        Bit = 0x03,
        Byte = 0x04,
        Integer = 0x05,
        Real = 0x07,
        OctetString = 0x09
    }
}