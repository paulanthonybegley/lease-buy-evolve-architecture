using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess.Database;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api.OfferPurchase;

internal static class OfferPurchaseEndpoint
{
    internal static void MapOfferPurchase(this IEndpointRouteBuilder app)
    {
        app.MapPost(PurchasingApiPaths.Offer, async (OfferPurchaseRequest request,
                PurchasingPersistence persistence, CancellationToken ct) =>
            {
                var purchase = Purchase.Offer(request.CustomerId, request.VehicleId, request.VehicleMsrp,
                    request.DownPayment, request.Apr, request.TermMonths, request.PreparedAt);
                await persistence.Purchases.AddAsync(purchase, ct);
                await persistence.SaveChangesAsync(ct);
                return Results.Created($"/{PurchasingApiPaths.Offer}/{purchase.Id}", purchase.Id);
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Offers a purchase option";
                operation.Description = "Offers a purchase (financing) option instead of leasing.";
                return Task.CompletedTask;
            })
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
