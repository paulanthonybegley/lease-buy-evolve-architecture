using System.Reflection;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Mediator;

public static class MediatorModule
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        return services;
    }
}
