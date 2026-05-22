namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}
