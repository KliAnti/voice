using Grpc.Core;

namespace voice.Services;

/// <summary>
/// Реализует методы gRPC-сервиса Voice.
/// </summary>
/// <param name="logger">Логгер для записи событий сервиса.</param>
public sealed class VoiceGrpcService(ILogger<VoiceGrpcService> logger) : Voice.VoiceBase
{
    /// <summary>
    /// Обрабатывает запрос Ping, записывает полученное сообщение в лог и возвращает Pong.
    /// </summary>
    /// <param name="request">Входящий запрос с сообщением.</param>
    /// <param name="context">Контекст текущего gRPC-вызова.</param>
    /// <returns>Ответ PongResponse с сообщением «Pong».</returns>
    public override Task<PongResponse> Ping(PingRequest request, ServerCallContext context)
    {
        logger.LogInformation("Received ping: {Message}", request.Message);

        return Task.FromResult(new PongResponse { Message = "Pong" });
    }
}
