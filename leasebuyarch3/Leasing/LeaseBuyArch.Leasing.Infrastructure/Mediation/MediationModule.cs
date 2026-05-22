using System.Reflection;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Mediator;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.PrepareLease;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure.Mediation;

internal static class MediationModule
{
    internal static IServiceCollection AddMediation(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(PrepareLeaseCommand).Assembly);
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }

    internal static IServiceCollection AddLeasingModule(this IServiceCollection services)
    {
        services.AddScoped<Application.ILeasingModule, LeasingModule>();
        return services;
    }
}
