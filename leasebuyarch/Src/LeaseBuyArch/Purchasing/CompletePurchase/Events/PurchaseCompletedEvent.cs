using EvolutionaryArchitecture.LeaseBuyArch.Common.Events;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.CompletePurchase.Events;

internal record PurchaseCompletedEvent(
    Guid Id,
    Guid PurchaseId,
    Guid CustomerId,
    Guid VehicleId,
    DateTimeOffset CompletedAt,
    DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PurchaseCompletedEvent Create(Guid purchaseId, Guid customerId,
        Guid vehicleId, DateTimeOffset completedAt) =>
        new(Guid.NewGuid(), purchaseId, customerId, vehicleId, completedAt, DateTimeOffset.UtcNow);
}
