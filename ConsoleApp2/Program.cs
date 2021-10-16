using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Sally7;
using Sally7.Plc;

#if !DEBUG
using BenchmarkDotNet.Running;
#endif

Bench bench = new();
bench.GlobalSetup();
try
{
    await bench.ReadViaSocketAwaitable();
    await bench.ReadViaValueTask();
}
finally
{
    bench.GlobalCleanup();
}

#if !DEBUG
BenchmarkRunner.Run<Bench>();
#endif

[MemoryDiagnoser]
public class Bench
{
    private const string PlcAddress = "10.0.0.20";
    private readonly IDataItem[] _dataItems;

    private S7Connection _connection;
    private S7ConnectionValueTask _connectionValueTask;

    public Bench()
    {
        DataBlockDataItem<short> t0 = new() { DbNumber = 102, StartByte = 2 * 19 };
        DataBlockDataItem<short> t1 = new() { DbNumber = 102, StartByte = 2 * 21 };
        DataBlockDataItem<short> t2 = new() { DbNumber = 102, StartByte = 2 * 23 };

        _dataItems = new[] { t0, t1, t2 };
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _connection = ConnectionFactory.GetConnection(PlcAddress, CpuType.S7_300, 0, 1);
        _connection.OpenAsync().GetAwaiter().GetResult();

        _connectionValueTask = ConnectionFactory.GetConnectionValueTask(PlcAddress, CpuType.S7_300, 0, 1);
        _connectionValueTask.OpenAsync().GetAwaiter().GetResult();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _connection.Close();
        _connection.Dispose();

        _connectionValueTask.Close();
        _connectionValueTask.Dispose();
    }

    [Benchmark(Baseline = true)]
    public async ValueTask ReadViaSocketAwaitable()
    {
        await _connection.ReadAsync(_dataItems);
    }

    [Benchmark]
    public async ValueTask ReadViaValueTask()
    {
        await _connectionValueTask.ReadAsync(_dataItems);
    }
}
