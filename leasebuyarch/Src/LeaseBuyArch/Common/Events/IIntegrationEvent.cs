using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Events;

internal interface IIntegrationEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredDateTime { get; }
}
