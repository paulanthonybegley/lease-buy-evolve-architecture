namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.GetAllPurchases;

internal sealed record GetAllPurchasesResponse(
    Guid Id,
    Guid CustomerId,
    Guid VehicleId,
    decimal VehicleMsrp,
    decimal MonthlyPayment,
    int TermMonths,
    DateTimeOffset PreparedAt,
    DateTimeOffset? CompletedAt);
