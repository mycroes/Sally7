namespace Sally7.Protocol.S7
{
    internal enum ParameterErrorCode : ushort
    {
        NoError = 0x0000,
        InvalidBlockTypeNumber = 0x0110,
        InvalidParameter = 0x0112,
        PgResourceError = 0x011A,
        PlcResourceError = 0x011B,
        ProtocolError = 0x011C,
        UserBufferTooShort = 0x011F,
        RequestError = 0x0141,
        VersionMismatch = 0x01C0,
        NotImplement = 0x01F0,
        L7InvalidCpuState = 0x8001,
        L7PduSizeError = 0x8500,
        L7InvalidSzlId = 0xD401,
        L7InvalidIndex = 0xD402,
        L7DgsConnectionAlreadyAnnounced = 0xD403,
        L7MaxUserNb = 0xD404,
        L7DgsFunctionParameterSyntaxError = 0xD405,
        L7NoInfo = 0xD406,
        L7PrtFunctionParameterSyntaxError = 0xD601,
        L7InvalidVariableAddress = 0xD801,
        L7UnknownRequest = 0xD802,
        L7InvalidRequestStatus = 0xD803
    }
}