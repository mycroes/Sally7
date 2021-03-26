using System;
using System.Runtime.InteropServices;

namespace Sally7.Protocol.Cotp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Data
    {
        public byte Length;
        public byte DataIdentifier;
        public byte PduNumberAndEot;

        public void Init()
        {
            Length = 2;
            DataIdentifier = 0b1111_0000;
            PduNumberAndEot = 0b1_000_0000;
        }

        public readonly void Assert()
        {
            if (Length != 2) throw new Exception($"Expected length of 2, received {Length}.");
            if (DataIdentifier != 0b1111_0000) throw new Exception($"Invalid DT identifier, expected {0b1111_0000}, received {DataIdentifier}.");
            if ((PduNumberAndEot & 0b1_000_0000) == 0) throw new Exception("Expected a single PDU return only.");
        }
    }
}