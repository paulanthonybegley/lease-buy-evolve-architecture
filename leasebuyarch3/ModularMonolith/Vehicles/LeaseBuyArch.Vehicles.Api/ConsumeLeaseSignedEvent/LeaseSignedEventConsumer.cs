using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.IntegrationEvents;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;
using MassTransit;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.ConsumeLeaseSignedEvent;

internal sealed class LeaseSignedEventConsumer : IConsumer<LeaseSignedEvent>
{
    private readonly VehiclesPersistence _persistence;
    private readonly IEventBus _eventBus;

    public LeaseSignedEventConsumer(VehiclesPersistence persistence, IEventBus eventBus)
    { _persistence = persistence; _eventBus = eventBus; }

    public async Task Consume(ConsumeContext<LeaseSignedEvent> context)
    {
        var @event = context.Message;
        var vehicle = await _persistence.Vehicles.FindAsync(new object?[] { @event.VehicleId }, context.CancellationToken);
        if (vehicle is null) return;
        vehicle.MarkAsLeased();
        await _persistence.SaveChangesAsync(context.CancellationToken);
    }
}
