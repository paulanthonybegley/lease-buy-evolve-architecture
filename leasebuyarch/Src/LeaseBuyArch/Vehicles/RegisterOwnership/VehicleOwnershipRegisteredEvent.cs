using EvolutionaryArchitecture.LeaseBuyArch.Common.Events;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.RegisterOwnership;

internal record VehicleOwnershipRegisteredEvent(
    Guid Id,
    Guid VehicleId,
    Guid CustomerId,
    string OwnershipType,
    DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static VehicleOwnershipRegisteredEvent Create(Guid vehicleId, Guid customerId, string ownershipType) =>
        new(Guid.NewGuid(), vehicleId, customerId, ownershipType, DateTimeOffset.UtcNow);
}
