using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data.Database;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Vehicles";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<VehiclesPersistence>(options => options.UseNpgsql(connectionString));

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
