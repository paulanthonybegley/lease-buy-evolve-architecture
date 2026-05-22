using System.Reflection;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus.InMemory;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
}
