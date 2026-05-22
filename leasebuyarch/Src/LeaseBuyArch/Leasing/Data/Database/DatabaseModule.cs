using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database;

internal static class DatabaseModule
{
    private const string ConnectionStringName = "Leasing";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<LeasingPersistence>(options => options.UseNpgsql(connectionString));

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();

        return applicationBuilder;
    }
}
