using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events;

public interface IIntegrationEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredDateTime { get; }
}
