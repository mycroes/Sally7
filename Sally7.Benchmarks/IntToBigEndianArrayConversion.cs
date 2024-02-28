using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    public class IntToBigEndianArrayConversion
    {
        private int _value = 1 | 2 << 8 | 3 << 16 | 4 << 24;

        [Params(true, false)]
        public bool IsLittleEndian { get; set; }

        [GlobalSetup]
        public void SetupLocalBitConverter()
        {
            LocalBitConverter.IsLittleEndian = IsLittleEndian;
        }

        [Benchmark]
        public byte[] BitConverterGetBytesAndArrayReverse()
        {
            var arr = BitConverter.GetBytes(_value);
            if (LocalBitConverter.IsLittleEndian) Array.Reverse(arr);

            return arr;
        }

        [Benchmark]
        public byte[] BitConverterGetBytes()
        {
            var arr = BitConverter.GetBytes(_value);

            return LocalBitConverter.IsLittleEndian ? new[] {arr[3], arr[2], arr[1], arr[0]} : arr;
        }

        [Benchmark]
        public byte[] Manual()
        {
            return new[] {(byte) (_value >> 24), (byte) (_value >> 16), (byte) (_value >> 8), (byte) _value};
        }

        [Benchmark]
        public byte[] FromSpan()
        {
            var span = new Span<int>(new [] { _value });
            var byteSpan = MemoryMarshal.AsBytes(span);

            return LocalBitConverter.IsLittleEndian
                ? new[] {byteSpan[3], byteSpan[2], byteSpan[1], byteSpan[0]}
                : new[] {byteSpan[0], byteSpan[1], byteSpan[2], byteSpan[3]};
        }

        private class LocalBitConverter
        {
            public static bool IsLittleEndian { get; set; }
        }
    }
}