using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    public class BigEndianArrayToIntConversion
    {
        public BigEndianArrayToIntConversion()
        {
            _byteValue = new byte[0];
        }

        [Params(1, 2, 3)]
        public int Value { get; set; }

        private byte[] _byteValue;

        [GlobalSetup]
        public void GlobalSetupData()
        {
            _byteValue = new[] {(byte) (Value >> 24), (byte) (Value >> 16), (byte) (Value >> 8), (byte) Value};
        }

        [Benchmark(Baseline = true)]
        public int Manual()
        {
            return _byteValue[0] << 24 | _byteValue[1] << 16 | _byteValue[2] << 8 | _byteValue[3];
        }
    }
}