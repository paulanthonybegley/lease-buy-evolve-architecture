using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess.Database;

public sealed class PurchasingPersistence : DbContext
{
    private const string Schema = "Purchasing";
    public PurchasingPersistence(DbContextOptions<PurchasingPersistence> options) : base(options) { }
    public DbSet<Purchase> Purchases => Set<Purchase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.Entity<Purchase>(builder =>
        {
            builder.ToTable("Purchases");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CustomerId).IsRequired();
            builder.Property(p => p.VehicleId).IsRequired();
            builder.Property(p => p.VehicleMsrp).IsRequired();
            builder.Property(p => p.DownPayment).IsRequired();
            builder.Property(p => p.Apr).IsRequired();
            builder.Property(p => p.TermMonths).IsRequired();
            builder.Property(p => p.MonthlyPayment).IsRequired();
            builder.Property(p => p.PreparedAt).IsRequired();
            builder.Property(p => p.CompletedAt).IsRequired(false);
        });
    }
}
