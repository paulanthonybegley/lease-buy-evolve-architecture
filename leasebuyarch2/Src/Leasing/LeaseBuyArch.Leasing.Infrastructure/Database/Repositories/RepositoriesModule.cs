using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Database.Repositories;

internal static class RepositoriesModule
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILeasingRepository, LeasingRepository>();
        return services;
    }
}
