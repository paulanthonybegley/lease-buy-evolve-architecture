using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.SignLease.Events;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Common.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation.Requests;
using Microsoft.AspNetCore.OpenApi;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.SignLease;

internal static class SignLeaseEndpoint
{
    internal static void MapSignLease(this IEndpointRouteBuilder app)
    {
        app.MapPatch(LeasingApiPaths.Sign, async (Guid id, SignLeaseRequest request,
                LeasingPersistence persistence, IEventBus bus, ISystemClock systemClock,
                CancellationToken cancellationToken) =>
            {
                var lease = await persistence.Leases.FindAsync(new object?[] { id }, cancellationToken);
                if (lease is null)
                    return Results.NotFound();

                lease.Sign(systemClock.Now);
                await persistence.SaveChangesAsync(cancellationToken);

                var @event = LeaseSignedEvent.Create(lease.Id, lease.CustomerId, lease.VehicleId,
                    lease.MonthlyPayment, lease.TermMonths, lease.SignedAt!.Value);
                await bus.PublishAsync(@event, cancellationToken);

                return Results.NoContent();
            })
            .ValidateRequest<SignLeaseRequest>()
            .AddOpenApiOperationTransformer((operation, context, ct) =>
            {
                operation.Summary = "Signs prepared lease agreement";
                operation.Description = "This endpoint is used to sign a prepared lease agreement by the customer.";
                return Task.CompletedTask;
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
