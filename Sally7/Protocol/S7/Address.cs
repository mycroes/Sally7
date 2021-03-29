using System.Runtime.InteropServices;

namespace Sally7.Protocol.S7
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Address
    {
        /// <summary>
        /// The size of the <see cref="Address"/> struct.
        /// </summary>
        public const int Size =
            sizeof(byte) + // High
            sizeof(byte) + // Mid
            sizeof(byte); // Low

        public byte High;
        public byte Mid;
        public byte Low;

        public void FromStartByteAndBit(int startByte, int bit)
        {
            var value = startByte * 8 + bit;
            Low = (byte) value;
            Mid = (byte) (value >> 8);
            High = (byte) (value >> 16);
        }

        public static implicit operator Address(int value)
        {
            return new Address
            {
                Low = (byte) value,
                Mid = (byte) (value >> 8),
                High = (byte) (value >> 16)
            };
        }
    }
}