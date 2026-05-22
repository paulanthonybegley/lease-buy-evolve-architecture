using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess.Database;

public static class DatabaseModule
{
    private const string ConnectionStringName = "Purchasing";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<PurchasingPersistence>(options => options.UseNpgsql(connectionString));
        return services;
    }

    public static WebApplication UseDatabase(this WebApplication applicationBuilder)
    {
        using var scope = applicationBuilder.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PurchasingPersistence>();
        context.Database.Migrate();
        return applicationBuilder;
    }
}
