using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sally7.Infrastructure;

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
                ThrowHelper.ThrowAssertFailLengthReceived(Length);
            }

            if (DataIdentifier != 0b1111_0000)
            {
                ThrowHelper.ThrowAssertFailDTIdentifier(DataIdentifier);
            }

            if ((PduNumberAndEot & 0b1_000_0000) == 0)
            {
                ThrowHelper.ThrowAssertFailSinglePDU();
            }
        }
    }
}