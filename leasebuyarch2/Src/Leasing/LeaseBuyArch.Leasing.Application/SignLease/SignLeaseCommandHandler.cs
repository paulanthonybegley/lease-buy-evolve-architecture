using MediatR;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.IntegrationEvents;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.SignLease;

internal sealed class SignLeaseCommandHandler : IRequestHandler<SignLeaseCommand>
{
    private readonly ILeasingRepository _repository;
    private readonly ISystemClock _systemClock;
    private readonly IEventBus _eventBus;

    public SignLeaseCommandHandler(ILeasingRepository repository, ISystemClock systemClock, IEventBus eventBus)
    { _repository = repository; _systemClock = systemClock; _eventBus = eventBus; }

    public async Task Handle(SignLeaseCommand command, CancellationToken cancellationToken)
    {
        var lease = await _repository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Lease with id {command.Id} not found");

        lease.Sign(_systemClock.Now);
        await _repository.SaveChangesAsync(cancellationToken);

        var @event = LeaseSignedEvent.Create(lease.Id, lease.CustomerId, lease.VehicleId,
            lease.MonthlyPayment, lease.TermMonths, lease.SignedAt!.Value);
        await _eventBus.PublishAsync(@event, cancellationToken);
    }
}
