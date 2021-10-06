using System;
using System.Runtime.CompilerServices;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Assert()
        {
            if (Length != 2)
            {
                Throw(Length);
                static void Throw(byte length) => throw new Exception($"Expected length of 2, received {length}.");
            }

            if (DataIdentifier != 0b1111_0000)
            {
                Throw(DataIdentifier);
                static void Throw(byte dataIdentifier) => throw new Exception($"Invalid DT identifier, expected {0b1111_0000}, received {dataIdentifier}.");
            }

            if ((PduNumberAndEot & 0b1_000_0000) == 0)
            {
                Throw();
                static void Throw() => throw new Exception("Expected a single PDU return only.");
            }
        }
    }
}