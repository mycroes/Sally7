using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    public class StructFromSpan
    {
        private byte[] _buffer = new byte[5];

        [Benchmark(Baseline = true)]
        public ref SomeStruct ByIndexReference()
        {
            return ref MemoryMarshal.Cast<byte, SomeStruct>(_buffer.AsSpan())[0];
        }

        [Benchmark]
        public ref SomeStruct ByGetReference()
        {
            return ref MemoryMarshal.GetReference(MemoryMarshal.Cast<byte, SomeStruct>(_buffer.AsSpan()));
        }
    }
}