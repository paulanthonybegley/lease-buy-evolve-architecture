using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Database;

public static class DatabaseModule
{
    private const string ConnectionStringName = "Leasing";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        services.AddDbContext<LeasingPersistence>(options => options.UseNpgsql(connectionString));
        return services;
    }

    public static WebApplication UseDatabase(this WebApplication applicationBuilder)
    {
        using var scope = applicationBuilder.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LeasingPersistence>();
        context.Database.Migrate();
        return applicationBuilder;
    }
}
