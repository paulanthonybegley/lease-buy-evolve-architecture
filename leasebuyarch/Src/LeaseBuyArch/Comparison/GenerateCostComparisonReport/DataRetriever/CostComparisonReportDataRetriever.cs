using Dapper;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.GenerateCostComparisonReport.DataRetriever;

using DataAccess;
using Dtos;

internal sealed class CostComparisonReportDataRetriever : ICostComparisonReportDataRetriever
{
    private readonly IDatabaseConnectionFactory _connectionFactory;

    public CostComparisonReportDataRetriever(IDatabaseConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public async Task<IReadOnlyCollection<CostComparisonDto>> GetReportAsync(int year, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.Create();

        const string sql =
            """
            SELECT
                EXTRACT(MONTH FROM l."PreparedAt") AS MonthOrder,
                TO_CHAR(l."PreparedAt", 'Month') AS MonthName,
                COALESCE(SUM(l."MonthlyPayment"), 0) AS TotalLeasePayments,
                COALESCE(p.TotalPurchasePayments, 0) AS TotalPurchasePayments,
                COALESCE(SUM(l."MonthlyPayment"), 0) - COALESCE(p.TotalPurchasePayments, 0) AS LeaseVsBuyDifference
            FROM "Leasing"."Leases" l
            LEFT JOIN (
                SELECT
                    EXTRACT(MONTH FROM "PreparedAt") AS MonthOrder,
                    SUM("MonthlyPayment") AS TotalPurchasePayments
                FROM "Purchasing"."Purchases"
                WHERE EXTRACT(YEAR FROM "PreparedAt") = @Year
                GROUP BY EXTRACT(MONTH FROM "PreparedAt")
            ) p ON EXTRACT(MONTH FROM l."PreparedAt") = p.MonthOrder
            WHERE EXTRACT(YEAR FROM l."PreparedAt") = @Year
            GROUP BY EXTRACT(MONTH FROM l."PreparedAt"), TO_CHAR(l."PreparedAt", 'Month'), p.TotalPurchasePayments
            ORDER BY MonthOrder
            """;

        var result = await connection.QueryAsync<CostComparisonDto>(
            new CommandDefinition(sql, new { Year = year }, cancellationToken: cancellationToken));

        return result.ToList().AsReadOnly();
    }
}
