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
    public void ReturnJobId_Does_Not_Throw_If_Disposed()
    {
        // Arrange
        var sut = new JobPool(1);
        var jobId = sut.RentJobIdAsync(CancellationToken.None).Result;
        sut.Dispose();

        // Act
        // Assert
        sut.ReturnJobId(jobId);
    }
}