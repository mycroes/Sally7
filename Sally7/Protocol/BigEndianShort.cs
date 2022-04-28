using System.Runtime.InteropServices;

namespace Sally7.Protocol
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BigEndianShort
    {
        public byte High;
        public byte Low;

        public static implicit operator BigEndianShort(int value) =>
            new BigEndianShort {High = (byte) (value >> 8), Low = (byte) value};

        public static implicit operator BigEndianShort(byte value) => new BigEndianShort {High = 0, Low = value};

        public static implicit operator int(BigEndianShort ns) => ns.High << 8 | ns.Low;

        public override string ToString() => ((int) this).ToString();
    }
}