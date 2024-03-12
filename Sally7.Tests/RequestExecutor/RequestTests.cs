using FakeItEasy;
using Sally7.RequestExecutor;

namespace Sally7.Tests.RequestExecutor;

public class RequestTests
{
    [Fact]
    public void Completes_On_Dispose()
    {
        // Arrange
        var sut = new Request();
        var callback = A.Fake<Action>();
        sut.OnCompleted(callback);

        // Act
        sut.Dispose();

        // Assert
        A.CallTo(() => callback.Invoke()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Throws_When_Awaited_After_Dispose()
    {
        // Arrange
        var sut = new Request();

        // Act
        sut.Dispose();

        // Assert
        await Should.ThrowAsync<ObjectDisposedException>(async () => await sut);
    }
}