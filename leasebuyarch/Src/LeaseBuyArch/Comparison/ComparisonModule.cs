using EvolutionaryArchitecture.LeaseBuyArch.Comparison.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison;

internal static class ComparisonModule
{
    internal static IServiceCollection AddComparison(this IServiceCollection services)
    {
        services.AddDatabaseAccess();
        services.AddGenerateNewPassesPerMonthReport();

        return services;
    }

    internal static IApplicationBuilder UseComparison(this IApplicationBuilder applicationBuilder) => applicationBuilder;
}
