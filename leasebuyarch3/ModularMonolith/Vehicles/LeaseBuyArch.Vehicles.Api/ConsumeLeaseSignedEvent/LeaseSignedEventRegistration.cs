namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.ConsumeLeaseSignedEvent;

using Common.Infrastructure.Events.EventBus.Consumers;
using Microsoft.Extensions.DependencyInjection;

internal static class LeaseSignedEventRegistration
{
    internal static IServiceCollection RegisterLeaseSignedEventConsumer(this IServiceCollection services)
    {
        services.RegisterConsumer("leases-signed", typeof(LeaseSignedEventConsumer));
        return services;
    }
}
