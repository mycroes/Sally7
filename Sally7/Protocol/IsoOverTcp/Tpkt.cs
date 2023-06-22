using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sally7.Infrastructure;

namespace Sally7.Protocol.IsoOverTcp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Tpkt
    {
        private const byte IsoVersion = 3;

        public byte Version;
        public byte Reserved;
        public BigEndianShort Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Assert()
        {
            if (Version != IsoVersion)
            {
                TpktException.ThrowIncorrectHeaderVersion();
            }

            if (Reserved != 0)
            {
                TpktException.ThrowReseveredNot0();
            }

            if (Length.High == 0 && Length.Low < 7)
            {
                TpktException.ThrowLengthSmallerThan7();
            }
        }

        public readonly int MessageLength() => Length - 4;
    }
}