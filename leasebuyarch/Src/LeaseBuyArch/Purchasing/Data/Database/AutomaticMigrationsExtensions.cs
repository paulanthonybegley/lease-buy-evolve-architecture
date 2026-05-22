using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PurchasingPersistence>();
        context.Database.Migrate();

        return applicationBuilder;
    }
}
