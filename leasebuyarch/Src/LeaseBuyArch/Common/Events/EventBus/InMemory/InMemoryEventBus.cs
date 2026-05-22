using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus.InMemory;

internal sealed class InMemoryEventBus : IEventBus
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InMemoryEventBus(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Publish(@event, cancellationToken);
    }
}
