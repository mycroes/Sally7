using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    public class TypeChanging
    {
        [Params(1)]
        public int Value { get; set; }

        [Benchmark(Baseline = true)]
        public int RawValueReturn()
        {
            return Value;
        }

        [Benchmark]
        public Numbers CastToEnumReturn()
        {
            return (Numbers) Value;
        }

        [Benchmark]
        public Numbers UnsafeAsEnumReturn()
        {
            var val = Value;
            return Unsafe.As<int, Numbers>(ref val);
        }

        [Benchmark]
        public int UnsafeAsIntReturn()
        {
            var val = Value;
            return Unsafe.As<int, int>(ref val);
        }
    }
}