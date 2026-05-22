using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Database;

public sealed class VehiclesPersistence : DbContext
{
    private const string Schema = "Vehicles";
    public VehiclesPersistence(DbContextOptions<VehiclesPersistence> options) : base(options) { }
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.Entity<Vehicle>(builder =>
        {
            builder.ToTable("Vehicles");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Make).IsRequired();
            builder.Property(v => v.Model).IsRequired();
            builder.Property(v => v.Year).IsRequired();
            builder.Property(v => v.Msrp).IsRequired();
            builder.Property(v => v.ResidualPercentageAt36Months).IsRequired();
            builder.Property(v => v.Status).IsRequired();
        });
    }
}
