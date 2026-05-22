using EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport.DataRetriever;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport;

internal static class GenerateCostComparisonReportModule
{
    internal static IServiceCollection AddGenerateNewPassesPerMonthReport(this IServiceCollection services) =>
        services.AddSingleton<ICostComparisonReportDataRetriever, CostComparisonReportDataRetriever>();
}
