using EvolutionaryArchitecture.LeaseBuyArch.Common.Core;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Modules;

internal static class ModuleAvailabilityChecker
{
    internal static bool IsModuleEnabled(this IConfiguration configuration, Module module)
    {
        var featureManagement = configuration.GetSection("FeatureManagement");
        return featureManagement.Get<bool>(module.Value);
    }

    internal static bool IsModuleEnabled(this IApplicationBuilder app, Module module)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        return configuration.IsModuleEnabled(module);
    }
}
