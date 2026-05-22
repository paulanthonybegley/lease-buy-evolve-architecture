using System.Reflection;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Mediation;

internal static class MediationModule
{
    internal static IServiceCollection AddMediation(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }

    internal static IServiceCollection AddLeasingModule(this IServiceCollection services)
    {
        services.AddScoped<ILeasingModule, LeasingModule>();
        return services;
    }
}
