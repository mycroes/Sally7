using System;
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
                Throw(FunctionCode);
                static void Throw(FunctionCode actual) => throw new Exception($"Expected FunctionCode {FunctionCode.Read}, received {actual}.");
            }

            if (ItemCount != count)
            {
                Throw(ItemCount, count);
                static void Throw(byte itemCount, byte count) => throw new Exception($"Expected ItemCount {count}, received {itemCount}.");
            }
        }
    }
}