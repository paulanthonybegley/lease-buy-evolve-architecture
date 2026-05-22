using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Modules;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api;

public static class VehiclesModule
{
    public static IServiceCollection AddVehicles(this IServiceCollection services, IConfiguration configuration, string module)
    {
        if (!configuration.IsModuleEnabled(module)) return services;
        services.AddDatabase(configuration);
        return services;
    }

    public static WebApplication RegisterVehicles(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module)) return app;
        app.UseDatabase();
        return app;
    }
}
