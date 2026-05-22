using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;

internal sealed class PurchasingPersistence : DbContext
{
    private const string Schema = "Purchasing";

    public PurchasingPersistence(DbContextOptions<PurchasingPersistence> options)
        : base(options)
    {
    }

    public DbSet<Purchase> Purchases => Set<Purchase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new PurchaseEntityConfiguration());
    }
}
