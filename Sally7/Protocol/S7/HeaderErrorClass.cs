namespace Sally7.Protocol.S7
{
    internal enum HeaderErrorClass : byte
    {
        NoError = 0x00,
        ApplicationRelationShipError = 0x81,
        ObjectDefiniationError = 0x82,
        NoResourcesAvailableError = 0x83,
        ErrorOnServiceProcessing = 0x84,
        ErrorOnSupplies = 0x85,
        AccessError = 0x87
    }
}