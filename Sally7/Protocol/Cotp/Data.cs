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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Assert()
        {
            if (Length != 2)
            {
                CotpProtocolException.ThrowInvalidLength(Length);
            }

            if (DataIdentifier != 0b1111_0000)
            {
                CotpProtocolException.ThrowInvalidDTIdentifier(DataIdentifier);
            }

            if ((PduNumberAndEot & 0b1_000_0000) == 0)
            {
                CotpProtocolException.ThrowNotASinglePDUReturn();
            }
        }
    }
}