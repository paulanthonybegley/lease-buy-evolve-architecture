using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database;

internal sealed class PurchaseEntityConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");
        builder.HasKey(purchase => purchase.Id);
        builder.Property(purchase => purchase.CustomerId).IsRequired();
        builder.Property(purchase => purchase.VehicleId).IsRequired();
        builder.Property(purchase => purchase.VehicleMsrp).IsRequired();
        builder.Property(purchase => purchase.DownPayment).IsRequired();
        builder.Property(purchase => purchase.Apr).IsRequired();
        builder.Property(purchase => purchase.TermMonths).IsRequired();
        builder.Property(purchase => purchase.MonthlyPayment).IsRequired();
        builder.Property(purchase => purchase.PreparedAt).IsRequired();
        builder.Property(purchase => purchase.CompletedAt).IsRequired(false);
    }
}
