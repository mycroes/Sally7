using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
                S7ProtocolException.ThrowUnexpectedFunctionCode(FunctionCode.Read, FunctionCode);
            }

            if (ItemCount != count)
            {
                S7ProtocolException.ThrowUnexpectedItemCount(count, ItemCount);
            }
        }
    }
}