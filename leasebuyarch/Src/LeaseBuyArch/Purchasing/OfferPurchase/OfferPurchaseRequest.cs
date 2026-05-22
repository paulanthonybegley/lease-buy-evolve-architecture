namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.OfferPurchase;

public sealed record OfferPurchaseRequest(
    Guid CustomerId,
    Guid VehicleId,
    decimal VehicleMsrp,
    decimal DownPayment,
    decimal Apr,
    int TermMonths,
    DateTimeOffset PreparedAt);
