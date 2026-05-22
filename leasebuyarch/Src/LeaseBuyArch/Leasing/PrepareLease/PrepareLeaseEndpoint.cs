using EvolutionaryArchitecture.LeaseBuyArch.Common.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation.Requests;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database;
using FluentValidation;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.PrepareLease;

internal static class PrepareLeaseEndpoint
{
    internal static void MapPrepareLease(this IEndpointRouteBuilder app)
    {
        app.MapPost(LeasingApiPaths.Prepare, async (PrepareLeaseRequest request,
                IValidator<PrepareLeaseRequest> validator, LeasingPersistence persistence,
                ISystemClock systemClock, CancellationToken cancellationToken) =>
            {
                var previousLease = await GetPreviousForCustomerAsync(persistence, request.CustomerId, cancellationToken);
                var lease = Lease.Prepare(request.CustomerId, request.VehicleId, request.VehicleMsrp,
                    request.ResidualPercentage, request.MoneyFactor, request.TermMonths,
                    request.AnnualMileageLimit, request.CreditScore, systemClock.Now,
                    previousLease?.Signed);
                await persistence.Leases.AddAsync(lease, cancellationToken);
                await persistence.SaveChangesAsync(cancellationToken);

                return Results.Created($"/{LeasingApiPaths.Prepare}/{lease.Id}", lease.Id);
            })
            .ValidateRequest<PrepareLeaseRequest>()
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Triggers preparation of a new lease agreement";
                operation.Description = "This endpoint is used to prepare a new lease agreement for a customer.";
                return Task.CompletedTask;
            })
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    private static async Task<Lease?> GetPreviousForCustomerAsync(LeasingPersistence persistence, Guid customerId,
        CancellationToken cancellationToken = default) =>
        await persistence.Leases
            .OrderByDescending(lease => lease.PreparedAt)
            .SingleOrDefaultAsync(lease => lease.CustomerId == customerId, cancellationToken);
}
