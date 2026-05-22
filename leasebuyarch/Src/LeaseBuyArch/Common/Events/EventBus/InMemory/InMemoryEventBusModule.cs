using System.Reflection;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus.InMemory;

internal static class InMemoryEventBusModule
{
    internal static IServiceCollection AddInMemoryEventBus(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
