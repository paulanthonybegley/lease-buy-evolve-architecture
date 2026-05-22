using Microsoft.EntityFrameworkCore;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Database.Repositories;

internal sealed class LeasingRepository : ILeasingRepository
{
    private readonly LeasingPersistence _persistence;

    public LeasingRepository(LeasingPersistence persistence) => _persistence = persistence;

    public Task<Lease?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _persistence.Leases.FindAsync(new object?[] { id }, cancellationToken).AsTask();

    public Task<Lease?> GetPreviousForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default) =>
        _persistence.Leases
            .OrderByDescending(l => l.PreparedAt)
            .SingleOrDefaultAsync(l => l.CustomerId == customerId, cancellationToken);

    public async Task AddAsync(Lease lease, CancellationToken cancellationToken = default) =>
        await _persistence.Leases.AddAsync(lease, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _persistence.SaveChangesAsync(cancellationToken);
}
