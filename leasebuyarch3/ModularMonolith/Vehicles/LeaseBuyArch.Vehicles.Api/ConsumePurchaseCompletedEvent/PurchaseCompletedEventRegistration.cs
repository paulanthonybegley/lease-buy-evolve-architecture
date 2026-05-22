namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.ConsumePurchaseCompletedEvent;

using Common.Infrastructure.Events.EventBus.Consumers;
using Microsoft.Extensions.DependencyInjection;

internal static class PurchaseCompletedEventRegistration
{
    internal static IServiceCollection RegisterPurchaseCompletedEventConsumer(this IServiceCollection services)
    {
        services.RegisterConsumer("purchases-completed", typeof(PurchaseCompletedEventConsumer));
        return services;
    }
}
