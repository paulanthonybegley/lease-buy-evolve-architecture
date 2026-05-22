using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LeasingPersistence>();
        context.Database.Migrate();

        return applicationBuilder;
    }
}
