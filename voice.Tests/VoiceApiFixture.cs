using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TestFramework.DependencyInjection;
using TestFramework.Fixtures;
using voice.Services;

namespace voice.Tests;

/// <summary>
/// Поднимает Voice в памяти и подменяет логгер для проверки записей Ping.
/// </summary>
public sealed class VoiceApiFixture : MockFixtureBase<VoiceGrpcService>
{
    public Mock<ILogger<VoiceGrpcService>> Logger { get; private set; } = null!;

    protected override void ConfigureMocks(IServiceCollection services)
    {
        Logger = services.RegisterMock<ILogger<VoiceGrpcService>>(MockRegistry);
    }
}

/// <summary>Объединяет тесты Voice API в общую фикстуру.</summary>
[CollectionDefinition(Name)]
public sealed class VoiceApiCollection : ICollectionFixture<VoiceApiFixture>
{
    public const string Name = "Voice API";
}