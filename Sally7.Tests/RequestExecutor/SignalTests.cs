using System.Threading.Channels;
using Sally7.RequestExecutor;

namespace Sally7.Tests.RequestExecutor;

public class SignalTests
{
    [Fact]
    public async Task WaitAsync_Throws_If_Disposed()
    {
        // Arrange
        var sut = new Signal();
        sut.Dispose();

        // Act
        // Assert
        await Should.ThrowAsync<ChannelClosedException>(() => sut.WaitAsync(CancellationToken.None).AsTask());
    }
}