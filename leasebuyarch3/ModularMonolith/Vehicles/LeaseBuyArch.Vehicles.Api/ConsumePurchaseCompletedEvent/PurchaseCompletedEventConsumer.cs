using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.IntegrationEvents;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;
using MassTransit;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.ConsumePurchaseCompletedEvent;

internal sealed class PurchaseCompletedEventConsumer : IConsumer<PurchaseCompletedEvent>
{
    private readonly VehiclesPersistence _persistence;

    public PurchaseCompletedEventConsumer(VehiclesPersistence persistence) => _persistence = persistence;

    public async Task Consume(ConsumeContext<PurchaseCompletedEvent> context)
    {
        var @event = context.Message;
        var vehicle = await _persistence.Vehicles.FindAsync(new object?[] { @event.VehicleId }, context.CancellationToken);
        if (vehicle is null) return;
        vehicle.MarkAsOwned();
        await _persistence.SaveChangesAsync(context.CancellationToken);
    }
}
