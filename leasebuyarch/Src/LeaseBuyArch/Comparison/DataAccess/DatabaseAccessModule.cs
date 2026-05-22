namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.DataAccess;

internal static class DatabaseAccessModule
{
    private const string ConnectionStringName = "Comparison";

    internal static IServiceCollection AddDatabaseAccess(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseConnectionFactory>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            return new DatabaseConnectionFactory(connectionString!);
        });

        return services;
    }
}
