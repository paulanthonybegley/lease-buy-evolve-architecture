using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing;

internal static class PurchasingModule
{
    internal static IServiceCollection AddPurchasing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    internal static IApplicationBuilder UsePurchasing(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
