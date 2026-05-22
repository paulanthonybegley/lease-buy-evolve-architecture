using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.IntegrationEvents;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.RegisterOwnership;

internal sealed class LeaseSignedEventHandler : IIntegrationEventHandler<LeaseSignedEvent>
{
    private readonly VehiclesPersistence _persistence;
    private readonly IEventBus _eventBus;

    public LeaseSignedEventHandler(VehiclesPersistence persistence, IEventBus eventBus)
    { _persistence = persistence; _eventBus = eventBus; }

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
