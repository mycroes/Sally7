using System.Net;
using Sally7.Protocol.Cotp;
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
}