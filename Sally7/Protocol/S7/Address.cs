﻿using System.Runtime.InteropServices;

namespace Sally7.Protocol.S7
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Address
    {
        public byte High;
        public byte Mid;
        public byte Low;

        public static Address FromStartByteAndBit(int startByte, int bit)
        {
            var value = startByte * 8 + bit;
            return new Address { Low = (byte)value, Mid = (byte)(value >> 8), High = (byte)(value >> 16) };
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