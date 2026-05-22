using Microsoft.EntityFrameworkCore;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Database.Repositories;

internal sealed class LeasingRepository : ILeasingRepository
{
    private readonly LeasingPersistence _persistence;

    public LeasingRepository(LeasingPersistence persistence) => _persistence = persistence;

    public async Task<Lease?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _persistence.Leases.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

    public async Task<Lease?> GetPreviousForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default) =>
        await _persistence.Leases
            .Where(l => l.CustomerId == customerId)
            .OrderByDescending(l => l.PreparedAt)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task AddAsync(Lease lease, CancellationToken cancellationToken = default) =>
        await _persistence.Leases.AddAsync(lease, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _persistence.SaveChangesAsync(cancellationToken);
}
