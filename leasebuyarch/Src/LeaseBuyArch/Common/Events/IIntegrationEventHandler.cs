using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Events;

internal interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent
{
}
