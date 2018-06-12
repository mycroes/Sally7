namespace Sally7.Protocol.S7
{
    public enum Area : byte
    {
        S200SystemInfo = 0x03,
        S200SystemFlags = 0x05,
        S200AnalogInput = 0x06,
        S200AnalogOutput = 0x07,
        S7Counters = 0x1c,
        S7Timers = 0x1d,
        IecCounters = 0x1e,
        IecTimers = 0x1f,
        DirectPeripheralAccess = 0x80,
        Inputs = 0x81,
        Outputs = 0x82,
        Flags = 0x83,
        DataBlock = 0x84,
        InstanceDataBlock = 0x85,
        LocalData = 0x86,
        Unknown = 0x87
    }
}