using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database;

internal sealed class LeaseEntityConfiguration : IEntityTypeConfiguration<Lease>
{
    public void Configure(EntityTypeBuilder<Lease> builder)
    {
        builder.ToTable("Leases");
        builder.HasKey(lease => lease.Id);
        builder.Property(lease => lease.CustomerId).IsRequired();
        builder.Property(lease => lease.VehicleId).IsRequired();
        builder.Property(lease => lease.VehicleMsrp).IsRequired();
        builder.Property(lease => lease.ResidualPercentage).IsRequired();
        builder.Property(lease => lease.MoneyFactor).IsRequired();
        builder.Property(lease => lease.TermMonths).IsRequired();
        builder.Property(lease => lease.AnnualMileageLimit).IsRequired();
        builder.Property(lease => lease.MonthlyPayment).IsRequired();
        builder.Property(lease => lease.PreparedAt).IsRequired();
        builder.Property(lease => lease.SignedAt).IsRequired(false);
    }
}
