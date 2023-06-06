using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sally7.Protocol
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BigEndianShort
    {
        public byte High;
        public byte Low;

        public static implicit operator BigEndianShort(int intValue)
        {
            var value = BitConverter.IsLittleEndian
                ? BinaryPrimitives.ReverseEndianness((ushort)intValue)
                : (ushort)intValue;
            return Unsafe.As<ushort, BigEndianShort>(ref value);
        }

        public static implicit operator BigEndianShort(byte byteValue)
        {
            var value = BitConverter.IsLittleEndian
                ? BinaryPrimitives.ReverseEndianness((ushort)byteValue)
                : byteValue;
            return Unsafe.As<ushort, BigEndianShort>(ref value);
        }

        public static implicit operator int(BigEndianShort ns) => ns.High << 8 | ns.Low;

        public override string ToString() => ((int) this).ToString();
    }
}