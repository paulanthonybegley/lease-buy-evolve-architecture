using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;

public static class DatabaseModule
{
    private const string ConnectionStringName = "Vehicles";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<VehiclesPersistence>(options => options.UseNpgsql(connectionString));
        return services;
    }

    public static WebApplication UseDatabase(this WebApplication applicationBuilder)
    {
        using var scope = applicationBuilder.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<VehiclesPersistence>();
        context.Database.Migrate();
        return applicationBuilder;
    }
}
