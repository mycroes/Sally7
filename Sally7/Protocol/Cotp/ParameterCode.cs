namespace Sally7.Protocol.Cotp
{
    internal enum ParameterCode : byte
    {
        SourceTsap = 0b1100_0001,
        DestinationTsap = 0b1100_0010,
        TpduSize = 0b1100_0000,
    }
}