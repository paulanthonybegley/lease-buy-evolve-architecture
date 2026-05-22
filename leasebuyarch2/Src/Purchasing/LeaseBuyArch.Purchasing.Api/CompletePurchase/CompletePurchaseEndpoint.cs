using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess.Database;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.IntegrationEvents;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api.CompletePurchase;

internal static class CompletePurchaseEndpoint
{
    internal static void MapCompletePurchase(this IEndpointRouteBuilder app)
    {
        app.MapPatch(PurchasingApiPaths.Complete, async (Guid id, PurchasingPersistence persistence,
                IEventBus bus, ISystemClock systemClock, CancellationToken ct) =>
            {
                var purchase = await persistence.Purchases.FindAsync(new object?[] { id }, ct);
                if (purchase is null) return Results.NotFound();

                purchase.Complete(systemClock.Now);
                await persistence.SaveChangesAsync(ct);

                var @event = PurchaseCompletedEvent.Create(purchase.Id, purchase.CustomerId,
                    purchase.VehicleId, purchase.CompletedAt!.Value);
                await bus.PublishAsync(@event, ct);
                return Results.NoContent();
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Completes a purchase (loan paid off)";
                operation.Description = "Marks a vehicle purchase as completed when the loan is fully paid.";
                return Task.CompletedTask;
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
