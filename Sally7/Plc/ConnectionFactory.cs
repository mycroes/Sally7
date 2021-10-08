using Sally7.Protocol.Cotp;

namespace Sally7.Plc
{
    public static class ConnectionFactory
    {
        private static Tsap GetSourceTsap(CpuType cpuType)
        {
            switch (cpuType)
            {
                case CpuType.S7_200:
                    return new Tsap(0x10, 0x00);
                case CpuType.S7_300:
                case CpuType.S7_400:
                case CpuType.S7_1200:
                    return new Tsap(0x01, 0x00);
                case CpuType.S7_1500:
                    return new Tsap(0x10, 0x02);
                default:
                    Sally7SetupException.ThrowCpuTypeNotSupported(cpuType);
                    return default; // to make the compiler happy
            }
        }

        private static Tsap GetDestinationTsap(CpuType cpuType, int? rack, int? slot)
        {
            switch (cpuType)
            {
                case CpuType.S7_200:
                    return new Tsap(0x10, 0x00);
                case CpuType.S7_300:
                case CpuType.S7_400:
                case CpuType.S7_1200:
                case CpuType.S7_1500:
                    if (rack == null) Sally7SetupException.ThrowDestinationRackIsNull(cpuType, rack);
                    if (slot == null) Sally7SetupException.ThrowDestinationSlotIsNull(cpuType, slot);

                    return new Tsap(0x03, (byte) ((rack.GetValueOrDefault() << 5) | slot.GetValueOrDefault()));
                default:
                    Sally7SetupException.ThrowCpuTypeNotSupported(cpuType);
                    return default; // to make the compiler happy
            }
        }

        public static S7Connection GetConnection(string host, CpuType cpuType, int? rack = null, int? slot = null)
        {
            return new S7Connection(host, GetSourceTsap(cpuType), GetDestinationTsap(cpuType, rack, slot));
        }
    }
}
