using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.PrepareLease;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api.PrepareLease;

internal static class PrepareLeaseEndpoint
{
    internal static void MapPrepareLease(this IEndpointRouteBuilder app)
    {
        app.MapPost(LeasingApiPaths.Prepare, async (PrepareLeaseRequest request,
                ILeasingModule module, CancellationToken cancellationToken) =>
            {
                var command = new PrepareLeaseCommand(request.CustomerId, request.VehicleId,
                    request.VehicleMsrp, request.ResidualPercentage, request.MoneyFactor,
                    request.TermMonths, request.AnnualMileageLimit, request.CreditScore);
                var leaseId = await module.ExecuteCommandAsync(command, cancellationToken);
                return Results.Created($"/{LeasingApiPaths.Prepare}/{leaseId}", leaseId);
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Triggers preparation of a new lease agreement";
                operation.Description = "This endpoint is used to prepare a new lease agreement for a customer.";
                return Task.CompletedTask;
            })
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
