using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.IntegrationEvents;

public record PurchaseCompletedEvent(
    Guid Id, Guid PurchaseId, Guid CustomerId, Guid VehicleId,
    DateTimeOffset CompletedAt, DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    public static PurchaseCompletedEvent Create(Guid purchaseId, Guid customerId,
        Guid vehicleId, DateTimeOffset completedAt) =>
        new(Guid.NewGuid(), purchaseId, customerId, vehicleId, completedAt, DateTimeOffset.UtcNow);
}
