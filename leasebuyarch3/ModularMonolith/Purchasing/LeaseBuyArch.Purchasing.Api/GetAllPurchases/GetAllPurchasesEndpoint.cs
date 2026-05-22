using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api.GetAllPurchases;

internal static class GetAllPurchasesEndpoint
{
    internal static void MapGetAllPurchases(this IEndpointRouteBuilder app)
    {
        app.MapGet(PurchasingApiPaths.GetAll, async (PurchasingPersistence persistence, CancellationToken ct) =>
            {
                var purchases = await persistence.Purchases
                    .Select(p => new GetAllPurchasesResponse(p.Id, p.CustomerId, p.VehicleId,
                        p.VehicleMsrp, p.MonthlyPayment, p.TermMonths, p.PreparedAt, p.CompletedAt))
                    .ToListAsync(ct);
                return Results.Ok(purchases);
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Gets all purchases";
                operation.Description = "Returns all vehicle purchases.";
                return Task.CompletedTask;
            })
            .Produces<List<GetAllPurchasesResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
