using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Modules;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api;

public static class LeasingModule
{
    public static IServiceCollection AddLeasing(this IServiceCollection services, IConfiguration configuration, string module)
    {
        if (!configuration.IsModuleEnabled(module)) return services;
        services.AddLeasingInfrastructure(configuration);
        return services;
    }

    public static WebApplication RegisterLeasing(this WebApplication app, string module)
    {
        if (!app.IsModuleEnabled(module)) return app;
        app.RegisterLeasingInfrastructure();
        app.MapLeasing();
        return app;
    }
}
