namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.DataAccess;

public sealed class Purchase
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public Guid VehicleId { get; init; }
    public decimal VehicleMsrp { get; init; }
    public decimal DownPayment { get; init; }
    public decimal Apr { get; init; }
    public int TermMonths { get; init; }
    public decimal MonthlyPayment { get; init; }
    public DateTimeOffset PreparedAt { get; init; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public bool Completed => CompletedAt.HasValue;

    private Purchase(Guid id, Guid customerId, Guid vehicleId, decimal vehicleMsrp,
        decimal downPayment, decimal apr, int termMonths, decimal monthlyPayment, DateTimeOffset preparedAt)
    {
        Id = id; CustomerId = customerId; VehicleId = vehicleId; VehicleMsrp = vehicleMsrp;
        DownPayment = downPayment; Apr = apr; TermMonths = termMonths;
        MonthlyPayment = monthlyPayment; PreparedAt = preparedAt;
    }

    public static Purchase Offer(Guid customerId, Guid vehicleId, decimal vehicleMsrp,
        decimal downPayment, decimal apr, int termMonths, DateTimeOffset preparedAt)
    {
        var monthlyPayment = CalculateMonthlyPayment(vehicleMsrp, downPayment, apr, termMonths);
        return new Purchase(Guid.NewGuid(), customerId, vehicleId, vehicleMsrp,
            downPayment, apr, termMonths, monthlyPayment, preparedAt);
    }

    public void Complete(DateTimeOffset completedAt) => CompletedAt = completedAt;

    private static decimal CalculateMonthlyPayment(decimal msrp, decimal downPayment, decimal apr, int termMonths)
    {
        var loanAmount = msrp - downPayment;
        var monthlyRate = apr / 100 / 12;
        if (monthlyRate == 0) return Math.Round(loanAmount / termMonths, 2);
        var factor = (decimal)Math.Pow(1 + (double)monthlyRate, termMonths);
        var payment = loanAmount * (monthlyRate * factor) / (factor - 1);
        return Math.Round(payment, 2);
    }
}
