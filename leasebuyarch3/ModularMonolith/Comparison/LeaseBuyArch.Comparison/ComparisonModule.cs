using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Modules;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison.DataAccess;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison;

public static class ComparisonModule
{
    public static IServiceCollection AddComparison(this IServiceCollection services, IConfiguration configuration, string module)
    {
        if (!services.IsModuleEnabled(module)) return services;
        services.AddDatabaseAccess();
        services.AddGenerateCostComparisonReport();
        return services;
    }

    public static WebApplication RegisterComparison(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module)) return app;
        app.MapComparison();
        return app;
    }
}
