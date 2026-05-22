using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data.Database;

internal sealed class VehiclesPersistence : DbContext
{
    private const string Schema = "Vehicles";

    public VehiclesPersistence(DbContextOptions<VehiclesPersistence> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
    }
}
