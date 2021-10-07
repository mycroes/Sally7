using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sally7.Infrastructure;

namespace Sally7.Protocol.S7.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ReadRequest
    {
        public FunctionCode FunctionCode;
        public byte ItemCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Assert(byte count)
        {
            if (FunctionCode != FunctionCode.Read)
            {
                ThrowHelper.ThrowAssertFailFunctionCode(FunctionCode.Read, FunctionCode);
            }

            if (ItemCount != count)
            {
                ThrowHelper.ThrowAssertFailItemCount(count, ItemCount);
            }
        }
    }
}