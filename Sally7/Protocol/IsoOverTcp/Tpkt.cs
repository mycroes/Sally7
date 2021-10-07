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
                ThrowHelper.ThrowSpecViolationTPKTHeaderIncorrectVersion();
            }

            if (Reserved != 0)
            {
                ThrowHelper.ThrowSpecViolationTPKTReseveredNot0();
            }

            if (Length.High == 0 && Length.Low < 7)
            {
                ThrowHelper.ThrowSpecViolationTPKTLengthSmallerThan7();
            }
        }

        public void Init(BigEndianShort length)
        {
            Version = IsoVersion;
            Reserved = 0;
            Length = length;
        }

        public readonly int MessageLength() => Length - 4;
    }
}