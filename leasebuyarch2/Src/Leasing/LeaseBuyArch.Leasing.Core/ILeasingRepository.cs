namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;

public interface ILeasingRepository
{
    Task<Lease?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Lease?> GetPreviousForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task AddAsync(Lease lease, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
