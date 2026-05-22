using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation.Requests;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.OfferPurchase;

internal static class OfferPurchaseEndpoint
{
    internal static void MapOfferPurchase(this IEndpointRouteBuilder app)
    {
        app.MapPost(PurchasingApiPaths.Offer, async (OfferPurchaseRequest request,
                PurchasingPersistence persistence, CancellationToken cancellationToken) =>
            {
                var purchase = Purchase.Offer(request.CustomerId, request.VehicleId, request.VehicleMsrp,
                    request.DownPayment, request.Apr, request.TermMonths, request.PreparedAt);
                await persistence.Purchases.AddAsync(purchase, cancellationToken);
                await persistence.SaveChangesAsync(cancellationToken);

                return Results.Created($"/{PurchasingApiPaths.Offer}/{purchase.Id}", purchase.Id);
            })
            .ValidateRequest<OfferPurchaseRequest>()
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Offers a purchase option to the customer";
                operation.Description = "This endpoint offers a purchase (financing) option instead of leasing.";
                return Task.CompletedTask;
            })
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
