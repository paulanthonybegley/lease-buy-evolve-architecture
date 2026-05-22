using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.BusinessRules;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core.BusinessRules;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;

public sealed class Lease
{
    private const int MaxTermMonths = 36;
    private const int MaxAnnualMileage = 15000;
    private const int MinCreditScore = 700;

    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public Guid VehicleId { get; init; }
    public decimal VehicleMsrp { get; init; }
    public decimal ResidualPercentage { get; init; }
    public decimal MoneyFactor { get; init; }
    public int TermMonths { get; init; }
    public int AnnualMileageLimit { get; init; }
    public decimal MonthlyPayment { get; init; }
    public DateTimeOffset PreparedAt { get; init; }
    public DateTimeOffset? SignedAt { get; private set; }
    public bool Signed => SignedAt.HasValue;

    private Lease(Guid id, Guid customerId, Guid vehicleId, decimal vehicleMsrp,
        decimal residualPercentage, decimal moneyFactor, int termMonths,
        int annualMileageLimit, decimal monthlyPayment, DateTimeOffset preparedAt)
    {
        Id = id; CustomerId = customerId; VehicleId = vehicleId;
        VehicleMsrp = vehicleMsrp; ResidualPercentage = residualPercentage;
        MoneyFactor = moneyFactor; TermMonths = termMonths;
        AnnualMileageLimit = annualMileageLimit; MonthlyPayment = monthlyPayment;
        PreparedAt = preparedAt;
    }

    public static Lease Prepare(Guid customerId, Guid vehicleId, decimal vehicleMsrp,
        decimal residualPercentage, decimal moneyFactor, int termMonths,
        int annualMileageLimit, int creditScore, DateTimeOffset preparedAt,
        bool? isPreviousLeaseSettled = null)
    {
        BusinessRuleValidator.Validate(new CustomerCreditScoreMustBeHighEnoughRule(creditScore, MinCreditScore));
        BusinessRuleValidator.Validate(new MileageMustNotExceedMaxLimitRule(annualMileageLimit, MaxAnnualMileage));
        BusinessRuleValidator.Validate(new LeaseTermMustNotExceedMaxRule(termMonths, MaxTermMonths));
        BusinessRuleValidator.Validate(new PreviousLeaseMustBeSettledRule(isPreviousLeaseSettled));

        var monthlyPayment = CalculateMonthlyPayment(vehicleMsrp, residualPercentage, moneyFactor, termMonths);
        return new Lease(Guid.NewGuid(), customerId, vehicleId, vehicleMsrp,
            residualPercentage, moneyFactor, termMonths, annualMileageLimit,
            monthlyPayment, preparedAt);
    }

    public void Sign(DateTimeOffset signedAt)
    {
        BusinessRuleValidator.Validate(new LeaseCanOnlyBeSignedWithin14DaysFromPreparation(PreparedAt, signedAt));
        SignedAt = signedAt;
    }

    private static decimal CalculateMonthlyPayment(decimal msrp, decimal residualPercentage,
        decimal moneyFactor, int termMonths)
    {
        var residualValue = msrp * (residualPercentage / 100);
        var depreciation = msrp - residualValue;
        var depreciationFee = depreciation / termMonths;
        var rentCharge = (msrp + residualValue) * moneyFactor;
        return Math.Round(depreciationFee + rentCharge, 2);
    }
}
