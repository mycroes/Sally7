namespace Sally7.Protocol.S7
{
    internal enum FunctionCode : byte
    {
        Read = 0x04,
        Write = 0x05,
        CommunicationSetup = 0xf0
    }
}