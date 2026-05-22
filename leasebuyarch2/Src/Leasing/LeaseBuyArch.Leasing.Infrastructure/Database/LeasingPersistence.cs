using Microsoft.EntityFrameworkCore;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Database;

internal sealed class LeasingPersistence : DbContext
{
    private const string Schema = "Leasing";
    public LeasingPersistence(DbContextOptions<LeasingPersistence> options) : base(options) { }
    public DbSet<Lease> Leases => Set<Lease>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.Entity<Lease>(builder =>
        {
            builder.ToTable("Leases");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.CustomerId).IsRequired();
            builder.Property(l => l.VehicleId).IsRequired();
            builder.Property(l => l.VehicleMsrp).IsRequired();
            builder.Property(l => l.ResidualPercentage).IsRequired();
            builder.Property(l => l.MoneyFactor).IsRequired();
            builder.Property(l => l.TermMonths).IsRequired();
            builder.Property(l => l.AnnualMileageLimit).IsRequired();
            builder.Property(l => l.MonthlyPayment).IsRequired();
            builder.Property(l => l.PreparedAt).IsRequired();
            builder.Property(l => l.SignedAt).IsRequired(false);
        });
    }
}
