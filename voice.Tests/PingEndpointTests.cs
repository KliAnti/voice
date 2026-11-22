using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using voice.Grpc;

namespace voice.Tests;

/// <summary>
/// Проверяет gRPC API Voice на запрос Ping.
/// </summary>
[Collection(VoiceApiCollection.Name)]
public sealed class PingEndpointTests(VoiceApiFixture fixture)
{
    private readonly Voice.VoiceClient _client = fixture.CreateGrpcClient(
        channel => new Voice.VoiceClient(channel));

    [Theory]
    [InlineData("Ping")]
    [InlineData("some other message")]
    [InlineData("")]
    public async Task Ping_WhenCalled_ReturnsPong(string message)
    {
        // Arrange
        var request = new PingRequest { Message = message };

        // Act
        var response = await _client.PingAsync(request);

        // Assert
        response.Message.Should().Be("Pong");
    }

    [Fact]
    public async Task Ping_WhenCalled_LogsIncomingMessage()
    {
        // Arrange
        fixture.Logger.Invocations.Clear();
        const string message = "log me";

        // Act
        await _client.PingAsync(new PingRequest { Message = message });

        // Assert
        fixture.Logger.Verify(logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, _) => state.ToString() == $"Received ping: {message}"),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
