namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure;

using Events.EventBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class CommonInfrastructureModule
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventBus(configuration);
        return services;
    }
}
