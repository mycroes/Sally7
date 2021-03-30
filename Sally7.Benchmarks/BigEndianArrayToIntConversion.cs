using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    public class BigEndianArrayToIntConversion
    {
        public BigEndianArrayToIntConversion()
        {
            byteValue = new byte[0];
        }

        [Params(1, 2, 3)]
        public int Value { get; set; }

        private byte[] byteValue;

        [GlobalSetup]
        public void GlobalSetupData()
        {
            byteValue = new[] {(byte) (Value >> 24), (byte) (Value >> 16), (byte) (Value >> 8), (byte) Value};
        }

        [Benchmark(Baseline = true)]
        public int Manual()
        {
            return byteValue[0] << 24 | byteValue[1] << 16 | byteValue[2] << 8 | byteValue[3];
        }
    }
}