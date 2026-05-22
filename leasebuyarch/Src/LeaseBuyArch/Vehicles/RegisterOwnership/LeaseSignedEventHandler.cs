using EvolutionaryArchitecture.LeaseBuyArch.Common.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.SignLease.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.RegisterOwnership;

internal sealed class LeaseSignedEventHandler : IIntegrationEventHandler<LeaseSignedEvent>
{
    private readonly VehiclesPersistence _persistence;
    private readonly IEventBus _eventBus;

    public LeaseSignedEventHandler(VehiclesPersistence persistence, IEventBus eventBus)
    {
        _persistence = persistence;
        _eventBus = eventBus;
    }

    public async Task Handle(LeaseSignedEvent @event, CancellationToken cancellationToken)
    {
        var vehicle = await _persistence.Vehicles.FindAsync(new object?[] { @event.VehicleId }, cancellationToken);
        if (vehicle is null) return;

        vehicle.MarkAsLeased();
        await _persistence.SaveChangesAsync(cancellationToken);

        var ownershipEvent = VehicleOwnershipRegisteredEvent.Create(vehicle.Id, @event.CustomerId, "Lease");
        await _eventBus.PublishAsync(ownershipEvent, cancellationToken);
    }
}
