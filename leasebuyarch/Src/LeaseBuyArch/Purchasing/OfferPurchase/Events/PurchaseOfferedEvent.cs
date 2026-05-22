using EvolutionaryArchitecture.LeaseBuyArch.Common.Events;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.OfferPurchase.Events;

internal record PurchaseOfferedEvent(
    Guid Id,
    Guid PurchaseId,
    Guid CustomerId,
    Guid VehicleId,
    decimal MonthlyPayment,
    int TermMonths,
    DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    internal static PurchaseOfferedEvent Create(Guid purchaseId, Guid customerId, Guid vehicleId,
        decimal monthlyPayment, int termMonths) =>
        new(Guid.NewGuid(), purchaseId, customerId, vehicleId, monthlyPayment, termMonths, DateTimeOffset.UtcNow);
}
