using System.Threading.Tasks;
using Sally7;
using Sally7.Plc;

Bench bench = new();
bench.GlobalSetup();
try
{
    for (int i = 0; i < 20_000; ++i)
    {
        await bench.ReadViaSocketAwaitable();
    }
}
finally
{
    bench.GlobalCleanup();
}

#if !DEBUG

#endif

public class Bench
{
    private const string PlcAddress = "10.0.0.20";
    private readonly IDataItem[] _dataItems;

    private S7Connection _connection;

    public Bench()
    {
        DataBlockDataItem<short> t0 = new() { DbNumber = 102, StartByte = 2 * 19 };
        DataBlockDataItem<short> t1 = new() { DbNumber = 102, StartByte = 2 * 21 };
        DataBlockDataItem<short> t2 = new() { DbNumber = 102, StartByte = 2 * 23 };

        _dataItems = new[] { t0, t1, t2 };
    }

    public void GlobalSetup()
    {
        _connection = ConnectionFactory.GetConnection(PlcAddress, CpuType.S7_300, 0, 1);
        _connection.OpenAsync().GetAwaiter().GetResult();
    }

    public void GlobalCleanup()
    {
        _connection.Close();
        _connection.Dispose();
    }

    public async ValueTask ReadViaSocketAwaitable()
    {
        await _connection.ReadAsync(_dataItems);
    }
}
