namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport.DataRetriever;

internal interface ICostComparisonReportDataRetriever
{
    Task<IReadOnlyCollection<Dtos.CostComparisonDto>> GetReportAsync(int year, CancellationToken cancellationToken = default);
}
