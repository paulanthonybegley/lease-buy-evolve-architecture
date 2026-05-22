using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport;

internal static class GenerateCostComparisonReportEndpoint
{
    internal static void MapGenerateCostComparisonReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ComparisonApiPaths.Generate, async () =>
            {
                await Task.CompletedTask;
                return Results.Ok(new { Message = "Cost comparison report generation not yet implemented." });
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Generates a cost comparison report";
                operation.Description = "Compares leasing vs purchasing costs for a given vehicle and customer.";
                return Task.CompletedTask;
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
