namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport.Dtos;

internal sealed class CostComparisonDto
{
    public decimal MonthOrder { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public decimal TotalLeasePayments { get; set; }
    public decimal TotalPurchasePayments { get; set; }
    public decimal LeaseVsBuyDifference { get; set; }
}
