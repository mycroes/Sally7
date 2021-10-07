using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sally7.Infrastructure;

namespace Sally7.Protocol.Cotp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ConnectionConfirm
    {
        public static readonly byte Size = (byte) (Unsafe.SizeOf<ConnectionConfirm>() + 1);

        public byte ConnectionConfirmAndCredit;
        public BigEndianShort DestinationReference;
        public BigEndianShort SourceReference;
        public byte ClassAndOption;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Assert()
        {
            if (ConnectionConfirmAndCredit != 0b1101_0000)
            {
                ThrowHelper.ThrowSpecViolationConnectionConfirmDoesNotMatch();
            }
            //if (DestinationReference != 46) throw new Exception("Destination reference mismatch.");
            //if (SourceReference != 0) throw new Exception("Source reference mismatch.");
            if (ClassAndOption != 0)
            {
                ThrowHelper.ThrowSpecViolationOnlyClass0SupportedForTPKT();
            }
        }
    }
}