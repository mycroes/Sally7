using System.Runtime.InteropServices;

namespace Sally7.Benchmarks
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SomeStruct
    {
        public int IntValue;
        public byte ByteValue;
    }
}