using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Purchasing";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<PurchasingPersistence>(options => options.UseNpgsql(connectionString));

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
