namespace Sally7.Protocol.S7
{
    public enum VariableType : byte
    {
        Bit = 0x01,
        Byte = 0x02,
        Char = 0x03,
        Word = 0x04,
        Int = 0x05,
        DWord = 0x06,
        DInt = 0x07,
        Real = 0x08,
        Date = 0x09,
        TimeOfDay = 0x0a,
        Time = 0x0b,
        SSTime = 0x0c,
        DateTime = 0x0f,
        Counter = 0x1c,
        Timer = 0x1d,
        IecTimer = 0x1e,
        IecCounter = 0x1f,
        HsCounter = 0x20
    }
}