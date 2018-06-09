using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    public class Conversion
    {
        private int value = 1 | 2 << 8 | 3 << 16 | 4 << 24;

        [Benchmark]
        public byte[] BitConverterAndArrayReverse()
        {
            var arr = BitConverter.GetBytes(value);
            Array.Reverse(arr);

            return arr;
        }

        [Benchmark]
        public byte[] Manual()
        {
            return new[] {(byte) (value >> 24), (byte) (value >> 16), (byte) (value >> 8), (byte) value};
        }

        [Benchmark]
        public byte[] FromSpan()
        {
            var span = new Span<int>(new [] { value });
            var byteSpan = MemoryMarshal.AsBytes(span);
            return new[] {byteSpan[3], byteSpan[2], byteSpan[1], byteSpan[0]};
        }
    }
}