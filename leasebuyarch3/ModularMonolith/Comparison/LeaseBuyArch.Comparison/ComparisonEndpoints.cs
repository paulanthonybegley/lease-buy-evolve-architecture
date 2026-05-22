using EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison;

internal static class ComparisonEndpoints
{
    internal static void MapComparison(this IEndpointRouteBuilder app)
    {
        app.MapGenerateCostComparisonReport();
    }
}
