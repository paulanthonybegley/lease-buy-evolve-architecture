using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing;

internal static class LeasingModule
{
    internal static IServiceCollection AddLeasing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    internal static IApplicationBuilder UseLeasing(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
