using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.PrepareLease;

public sealed record PrepareLeaseCommand(
    Guid CustomerId, Guid VehicleId, decimal VehicleMsrp,
    decimal ResidualPercentage, decimal MoneyFactor, int TermMonths,
    int AnnualMileageLimit, int CreditScore) : ICommand<Guid>;
