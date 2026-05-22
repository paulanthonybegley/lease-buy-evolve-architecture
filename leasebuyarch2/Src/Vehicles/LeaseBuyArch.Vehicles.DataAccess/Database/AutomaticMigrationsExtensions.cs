using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<VehiclesPersistence>();
        context.Database.Migrate();
        return applicationBuilder;
    }
}
