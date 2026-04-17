using System.Net;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.S7;
using Xunit.Abstractions;

namespace Sally7.Tests.Protocol;

public class CommunicationTests
{
    private readonly ITestOutputHelper _output;

    public CommunicationTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task Verify_Open()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);

        var communication = new CommunicationSequence(_output)
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

        var communication = new CommunicationSequence(_output)
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

        var communication = new CommunicationSequence(_output)
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

    [Fact]
    public async Task Verify_Read_WithResults_DoesNotThrowAndReturnsErrorCode()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);
        var dataItem = new DataBlockDataItem<short>(9, 6);
        var results = new ReadWriteErrorCode[1];

        var communication = new CommunicationSequence(_output)
            .AddConnectRequest(PduSizeParameter.PduSize.Pdu1024, sourceTsap, destinationTsap)
            .AddCommunicationSetup()
            .AddRead(Area.DataBlock, 9, 6 << 3, 2, TransportSize.Byte, VariableType.Byte, [2, 1], ReadWriteErrorCode.AddressOutOfRange);

        async Task Client(int port)
        {
            var conn = new S7Connection(IPAddress.Loopback.ToString(), port, sourceTsap, destinationTsap);
            await conn.OpenAsync();
            await conn.ReadAsync([dataItem], results);
            conn.Close();
        }

        await Task.WhenAll(communication.Serve(out var port), Client(port));
        results[0].ShouldBe(ReadWriteErrorCode.AddressOutOfRange);
    }

    [Fact]
    public async Task Verify_Read_WithoutResults_ThrowsAggregateWithDataItemReadWriteException()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);
        var dataItem = new DataBlockDataItem<short>(9, 6);

        var communication = new CommunicationSequence(_output)
            .AddConnectRequest(PduSizeParameter.PduSize.Pdu1024, sourceTsap, destinationTsap)
            .AddCommunicationSetup()
            .AddRead(Area.DataBlock, 9, 6 << 3, 2, TransportSize.Byte, VariableType.Byte, [2, 1], ReadWriteErrorCode.AddressOutOfRange);

        async Task Client(int port)
        {
            var conn = new S7Connection(IPAddress.Loopback.ToString(), port, sourceTsap, destinationTsap);
            await conn.OpenAsync();

            var ex = await Should.ThrowAsync<AggregateException>(() => conn.ReadAsync(dataItem));
            ex.InnerExceptions.Count.ShouldBe(1);
            var itemEx = ex.InnerExceptions[0].ShouldBeOfType<DataItemReadWriteException>();
            itemEx.ErrorCode.ShouldBe(ReadWriteErrorCode.AddressOutOfRange);

            conn.Close();
        }

        await Task.WhenAll(communication.Serve(out var port), Client(port));
    }

    [Fact]
    public async Task Verify_Write_WithResults_DoesNotThrowAndReturnsErrorCode()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);
        var dataItem = new DataBlockDataItem<short>(9, 6) { Value = 513 };
        var results = new ReadWriteErrorCode[1];

        var communication = new CommunicationSequence(_output)
            .AddConnectRequest(PduSizeParameter.PduSize.Pdu1024, sourceTsap, destinationTsap)
            .AddCommunicationSetup()
            .AddWrite(Area.DataBlock, 9, 6 << 3, 2, TransportSize.Byte, VariableType.Byte, [2, 1], ReadWriteErrorCode.ObjectDoesNotExist);

        async Task Client(int port)
        {
            var conn = new S7Connection(IPAddress.Loopback.ToString(), port, sourceTsap, destinationTsap);
            await conn.OpenAsync();
            await conn.WriteAsync([dataItem], results);
            conn.Close();
        }

        await Task.WhenAll(communication.Serve(out var port), Client(port));
        results[0].ShouldBe(ReadWriteErrorCode.ObjectDoesNotExist);
    }

    [Fact]
    public async Task Verify_Write_WithoutResults_ThrowsAggregateWithDataItemReadWriteException()
    {
        var sourceTsap = new Tsap(201, 202);
        var destinationTsap = new Tsap(203, 204);
        var dataItem = new DataBlockDataItem<short>(9, 6) { Value = 513 };

        var communication = new CommunicationSequence(_output)
            .AddConnectRequest(PduSizeParameter.PduSize.Pdu1024, sourceTsap, destinationTsap)
            .AddCommunicationSetup()
            .AddWrite(Area.DataBlock, 9, 6 << 3, 2, TransportSize.Byte, VariableType.Byte, [2, 1], ReadWriteErrorCode.ObjectDoesNotExist);

        async Task Client(int port)
        {
            var conn = new S7Connection(IPAddress.Loopback.ToString(), port, sourceTsap, destinationTsap);
            await conn.OpenAsync();

            var ex = await Should.ThrowAsync<AggregateException>(() => conn.WriteAsync(dataItem));
            ex.InnerExceptions.Count.ShouldBe(1);
            var itemEx = ex.InnerExceptions[0].ShouldBeOfType<DataItemReadWriteException>();
            itemEx.ErrorCode.ShouldBe(ReadWriteErrorCode.ObjectDoesNotExist);

            conn.Close();
        }

        await Task.WhenAll(communication.Serve(out var port), Client(port));
    }
}
