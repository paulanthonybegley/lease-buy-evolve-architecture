using EvolutionaryArchitecture.LeaseBuyArch.Comparison.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Common.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport.Dtos;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport.DataRetriever;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport;

internal static class GenerateCostComparisonReportEndpoint
{
    internal static void MapGenerateCostComparisonReport(this IEndpointRouteBuilder app)
    {
        app.MapGet(ComparisonApiPaths.Generate, async (ICostComparisonReportDataRetriever dataRetriever,
                ISystemClock systemClock, CancellationToken cancellationToken) =>
            {
                var year = systemClock.Now.Year;
                var reportData = await dataRetriever.GetReportAsync(year, cancellationToken);

                return Results.Ok(new CostComparisonResponse(reportData));
            })
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Generates lease vs buy cost comparison report";
                operation.Description = "This endpoint generates a report comparing leasing vs buying costs for all vehicles.";
                return Task.CompletedTask;
            })
            .Produces<CostComparisonResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
