namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Modules;

using Microsoft.FeatureManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class ModuleAvailabilityChecker
{
    public static bool IsModuleEnabled(this IServiceCollection services, string module)
    {
        var serviceProvider = services.BuildServiceProvider();
        var featureManager = serviceProvider.GetRequiredService<IFeatureManager>();
        return featureManager.IsEnabledAsync(module).Result;
    }

    public static bool IsModuleEnabled(this IApplicationBuilder applicationBuilder, string module)
    {
        var serviceProvider = applicationBuilder.ApplicationServices;
        var featureManager = serviceProvider.GetRequiredService<IFeatureManager>();
        return featureManager.IsEnabledAsync(module).Result;
    }
}
