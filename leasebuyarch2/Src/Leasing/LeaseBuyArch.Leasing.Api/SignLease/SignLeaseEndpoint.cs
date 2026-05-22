using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.SignLease;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api.SignLease;

internal static class SignLeaseEndpoint
{
    internal static void MapSignLease(this IEndpointRouteBuilder app)
    {
        app.MapPatch(LeasingApiPaths.Sign, async (Guid id, ILeasingModule module,
                CancellationToken cancellationToken) =>
            {
                await module.ExecuteCommandAsync(new SignLeaseCommand(id), cancellationToken);
                return Results.NoContent();
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Signs prepared lease agreement";
                operation.Description = "This endpoint is used to sign a prepared lease agreement.";
                return Task.CompletedTask;
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
