namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api.PrepareLease;

public sealed record PrepareLeaseRequest(
    Guid CustomerId, Guid VehicleId, decimal VehicleMsrp,
    decimal ResidualPercentage, decimal MoneyFactor, int TermMonths,
    int AnnualMileageLimit, int CreditScore);
