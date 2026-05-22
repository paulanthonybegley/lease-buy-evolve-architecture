using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events;

public interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent
{
}
