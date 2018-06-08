namespace Sally7.Protocol.S7.Messages
{
    internal enum MessageType : byte
    {
        JobRequest = 0x01,
        Ack = 0x02,
        AckData = 0x03,
        UserData = 0x07
    }
}