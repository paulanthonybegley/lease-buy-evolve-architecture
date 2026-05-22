using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Events.EventBus;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure;

public static class CommonInfrastructureModule
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddEventBus();
        return services;
    }
}
