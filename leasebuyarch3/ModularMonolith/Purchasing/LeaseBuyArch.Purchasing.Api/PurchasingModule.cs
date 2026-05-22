using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Modules;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api;

public static class PurchasingModule
{
    public static IServiceCollection AddPurchasing(this IServiceCollection services, IConfiguration configuration, string module)
    {
        if (!services.IsModuleEnabled(module)) return services;
        services.AddDatabase(configuration);
        return services;
    }

    public static WebApplication RegisterPurchasing(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module)) return app;
        app.UseDatabase();
        app.MapPurchasing();
        return app;
    }
}
