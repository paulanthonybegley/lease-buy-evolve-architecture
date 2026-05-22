using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus.InMemory;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus;

public static class EventBusModule
{
    public static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus();
}
