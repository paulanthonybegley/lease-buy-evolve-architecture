namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.IntegrationEvents;

using Common.Infrastructure.Events;

public sealed record LeaseSignedEvent(
    Guid Id, Guid LeaseId, Guid CustomerId, Guid VehicleId,
    decimal MonthlyPayment, int TermMonths, DateTimeOffset SignedAt,
    DateTimeOffset OccurredDateTime) : IIntegrationEvent
{
    public static LeaseSignedEvent Create(Guid leaseId, Guid customerId, Guid vehicleId,
        decimal monthlyPayment, int termMonths, DateTimeOffset signedAt) =>
        new(Guid.NewGuid(), leaseId, customerId, vehicleId,
            monthlyPayment, termMonths, signedAt, DateTimeOffset.UtcNow);
}
