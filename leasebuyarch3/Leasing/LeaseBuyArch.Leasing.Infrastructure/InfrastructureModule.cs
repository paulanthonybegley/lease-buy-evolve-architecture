using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Database;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Database.Repositories;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Mediation;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddLeasingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddMediation();
        services.AddLeasingModule();
        return services;
    }

    public static WebApplication RegisterLeasingInfrastructure(this WebApplication app)
    {
        app.UseDatabase();
        return app;
    }
}
