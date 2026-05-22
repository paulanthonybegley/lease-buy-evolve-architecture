using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.IntegrationEvents;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.RegisterOwnership;

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
