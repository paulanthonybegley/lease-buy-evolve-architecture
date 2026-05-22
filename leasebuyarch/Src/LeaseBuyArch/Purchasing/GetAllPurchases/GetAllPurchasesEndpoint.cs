using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.GetAllPurchases;

internal static class GetAllPurchasesEndpoint
{
    internal static void MapGetAllPurchases(this IEndpointRouteBuilder app)
    {
        app.MapGet(PurchasingApiPaths.GetAll, async (PurchasingPersistence persistence,
                CancellationToken cancellationToken) =>
            {
                var purchases = await persistence.Purchases
                    .Select(purchase => new GetAllPurchasesResponse(purchase.Id, purchase.CustomerId,
                        purchase.VehicleId, purchase.VehicleMsrp, purchase.MonthlyPayment,
                        purchase.TermMonths, purchase.PreparedAt, purchase.CompletedAt))
                    .ToListAsync(cancellationToken);

                return Results.Ok(purchases);
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Gets all purchases";
                operation.Description = "This endpoint returns all vehicle purchases.";
                return Task.CompletedTask;
            })
            .Produces<List<GetAllPurchasesResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
