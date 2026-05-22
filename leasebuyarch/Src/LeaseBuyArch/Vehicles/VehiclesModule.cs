using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data.Database;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles;

internal static class VehiclesModule
{
    internal static IServiceCollection AddVehicles(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    internal static IApplicationBuilder UseVehicles(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();

        return applicationBuilder;
    }
}
