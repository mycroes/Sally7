using System.Threading.Channels;
using Sally7.RequestExecutor;

namespace Sally7.Tests.RequestExecutor;

public class JobPoolTests
{
    [Fact]
    public async Task RentJobIdAsync_Throws_If_Disposed_And_Depleted()
    {
        // Arrange
        var sut = new JobPool(1);
        sut.Dispose();
        _ = await sut.RentJobIdAsync(CancellationToken.None); // Empty the pool

        // Act
        // Assert
        await Should.ThrowAsync<ChannelClosedException>(() => sut.RentJobIdAsync(CancellationToken.None).AsTask());
    }

    [Fact]
    public async Task ReturnJobId_Does_Not_Throw_If_Disposed()
    {
        // Arrange
        var sut = new JobPool(1);
        var jobId = await sut.RentJobIdAsync(CancellationToken.None);
        sut.Dispose();

        // Act
        // Assert
        sut.ReturnJobId(jobId);
    }

    [Fact]
    public async Task Dispose_Calls_Dispose_On_Requests()
    {
        // Arrange
        var sut = new JobPool(1);
        var jobId = await sut.RentJobIdAsync(CancellationToken.None);
        var request = sut.GetRequest(jobId);

        // Act
        sut.Dispose();

        // Assert
        Should.Throw<ObjectDisposedException>(() => request.GetResult());
    }
}