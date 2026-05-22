using MediatR;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.PrepareLease;

internal sealed class PrepareLeaseCommandHandler : IRequestHandler<PrepareLeaseCommand, Guid>
{
    private readonly ILeasingRepository _repository;
    private readonly ISystemClock _systemClock;

    public PrepareLeaseCommandHandler(ILeasingRepository repository, ISystemClock systemClock)
    { _repository = repository; _systemClock = systemClock; }

    public async Task<Guid> Handle(PrepareLeaseCommand command, CancellationToken cancellationToken)
    {
        var previousLease = await _repository.GetPreviousForCustomerAsync(command.CustomerId, cancellationToken);
        var lease = Lease.Prepare(command.CustomerId, command.VehicleId, command.VehicleMsrp,
            command.ResidualPercentage, command.MoneyFactor, command.TermMonths,
            command.AnnualMileageLimit, command.CreditScore, _systemClock.Now,
            previousLease?.Signed);
        await _repository.AddAsync(lease, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return lease.Id;
    }
}
