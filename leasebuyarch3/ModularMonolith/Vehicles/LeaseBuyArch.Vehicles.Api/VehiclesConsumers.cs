using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.ConsumeLeaseSignedEvent;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api.ConsumePurchaseCompletedEvent;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api;

internal static class VehiclesConsumers
{
    internal static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.RegisterLeaseSignedEventConsumer();
        services.RegisterPurchaseCompletedEventConsumer();
        return services;
    }
}
