using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api;

public static class LeasingModule
{
    public static IServiceCollection AddLeasing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLeasingInfrastructure(configuration);
        return services;
    }

    public static WebApplication RegisterLeasing(this WebApplication app)
    {
        app.RegisterLeasingInfrastructure();
        app.MapLeasing();
        return app;
    }
}
