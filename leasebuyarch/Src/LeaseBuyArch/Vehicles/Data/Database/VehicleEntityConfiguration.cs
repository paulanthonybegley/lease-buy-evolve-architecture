using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data.Database;

internal sealed class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");
        builder.HasKey(vehicle => vehicle.Id);
        builder.Property(vehicle => vehicle.Make).IsRequired();
        builder.Property(vehicle => vehicle.Model).IsRequired();
        builder.Property(vehicle => vehicle.Year).IsRequired();
        builder.Property(vehicle => vehicle.Msrp).IsRequired();
        builder.Property(vehicle => vehicle.ResidualPercentageAt36Months).IsRequired();
        builder.Property(vehicle => vehicle.Status).IsRequired();
    }
}
