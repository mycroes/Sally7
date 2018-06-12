using System;
using System.Runtime.InteropServices;

namespace Sally7.Protocol.S7.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ReadRequest
    {
        public FunctionCode FunctionCode;
        public byte ItemCount;

        public void Assert(byte count)
        {
            if (FunctionCode != FunctionCode.Read) throw new Exception($"Expected FunctionCode {FunctionCode.Read}, received {FunctionCode}.");
            if (ItemCount != count) throw new Exception($"Expected ItemCount {count}, received {ItemCount}.");
        }
    }
}