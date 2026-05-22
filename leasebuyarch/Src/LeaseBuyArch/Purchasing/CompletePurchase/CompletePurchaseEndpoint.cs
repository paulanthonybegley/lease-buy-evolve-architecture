using EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Common.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.CompletePurchase.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.CompletePurchase;

internal static class CompletePurchaseEndpoint
{
    internal static void MapCompletePurchase(this IEndpointRouteBuilder app)
    {
        app.MapPatch(PurchasingApiPaths.Complete, async (Guid id, PurchasingPersistence persistence,
                IEventBus bus, ISystemClock systemClock, CancellationToken cancellationToken) =>
            {
                var purchase = await persistence.Purchases.FindAsync(new object?[] { id }, cancellationToken);
                if (purchase is null)
                    return Results.NotFound();

                purchase.Complete(systemClock.Now);
                await persistence.SaveChangesAsync(cancellationToken);

                var @event = PurchaseCompletedEvent.Create(purchase.Id, purchase.CustomerId,
                    purchase.VehicleId, purchase.CompletedAt!.Value);
                await bus.PublishAsync(@event, cancellationToken);

                return Results.NoContent();
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Marks a purchase as completed (loan paid off)";
                operation.Description = "This endpoint marks a vehicle purchase as completed when the loan is fully paid.";
                return Task.CompletedTask;
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
