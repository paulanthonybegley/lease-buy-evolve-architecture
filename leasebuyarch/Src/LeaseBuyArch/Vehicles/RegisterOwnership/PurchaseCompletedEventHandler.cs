using EvolutionaryArchitecture.LeaseBuyArch.Common.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.CompletePurchase.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.RegisterOwnership;

internal sealed class PurchaseCompletedEventHandler : IIntegrationEventHandler<PurchaseCompletedEvent>
{
    private readonly VehiclesPersistence _persistence;

    public PurchaseCompletedEventHandler(VehiclesPersistence persistence) => _persistence = persistence;

    public async Task Handle(PurchaseCompletedEvent @event, CancellationToken cancellationToken)
    {
        var vehicle = await _persistence.Vehicles.FindAsync(new object?[] { @event.VehicleId }, cancellationToken);
        if (vehicle is null) return;

        vehicle.MarkAsOwned();
        await _persistence.SaveChangesAsync(cancellationToken);
    }
}
