using System.Net;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.S7;
using Xunit.Abstractions;

namespace Sally7.Tests.Protocol;

public class CommunicationTests
{
    private readonly ITestOutputHelper output;

    public CommunicationTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public async Task Verify_Open()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);

        var communication = new CommunicationSequence(output)
            .AddConnectRequest(PduSizeParameter.PduSize.Pdu1024, sourceTsap, destinationTsap)
            .AddCommunicationSetup();

        async Task Client(int port)
        {
            var conn = new S7Connection(IPAddress.Loopback.ToString(), port, sourceTsap, destinationTsap);
            await conn.OpenAsync();
            conn.Close();
        }

        await Task.WhenAll(communication.Serve(out var port), Client(port));
    }

    [Fact]
    public async Task Verify_Read_Single()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);
        var dataItem = new DataBlockDataItem<short>(9, 6);

        var communication = new CommunicationSequence(output)
            .AddConnectRequest(PduSizeParameter.PduSize.Pdu1024, sourceTsap, destinationTsap)
            .AddCommunicationSetup()
            .AddRead(Area.DataBlock, 9, 6 << 3, 2, TransportSize.Byte, VariableType.Byte, new byte[] { 2, 1});

        async Task Client(int port)
        {
            var conn = new S7Connection(IPAddress.Loopback.ToString(), port, sourceTsap, destinationTsap);
            await conn.OpenAsync();
            await conn.ReadAsync(dataItem);
            conn.Close();
        }

        await Task.WhenAll(communication.Serve(out var port), Client(port));
        dataItem.Value.ShouldBe((short) 513);
    }

    [Fact]
    public async Task Verify_Write_Single()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);
        var dataItem = new DataBlockDataItem<short>(9, 6) { Value = 513 };

        var communication = new CommunicationSequence(output)
            .AddConnectRequest(PduSizeParameter.PduSize.Pdu1024, sourceTsap, destinationTsap).AddCommunicationSetup()
            .AddWrite(Area.DataBlock, 9, 6 << 3, 2, TransportSize.Byte, VariableType.Byte, new byte[] { 2, 1 });

        async Task Client(int port)
        {
            var conn = new S7Connection(IPAddress.Loopback.ToString(), port, sourceTsap, destinationTsap);
            await conn.OpenAsync();
            await conn.WriteAsync(dataItem);
            conn.Close();
        }

        await Task.WhenAll(communication.Serve(out var port), Client(port));
    }
}