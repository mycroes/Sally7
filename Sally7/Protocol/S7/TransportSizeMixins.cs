namespace Sally7.Protocol.S7
{
    internal static class TransportSizeMixins
    {
        public static bool IsSizeInBytes(this TransportSize transportSize) =>
            transportSize == TransportSize.Bit || transportSize == TransportSize.OctetString ||
            transportSize == TransportSize.Real;
    }
}