using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database;

internal sealed class LeasingPersistence : DbContext
{
    private const string Schema = "Leasing";

    public LeasingPersistence(DbContextOptions<LeasingPersistence> options)
        : base(options)
    {
    }

    public DbSet<Lease> Leases => Set<Lease>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new LeaseEntityConfiguration());
    }
}
