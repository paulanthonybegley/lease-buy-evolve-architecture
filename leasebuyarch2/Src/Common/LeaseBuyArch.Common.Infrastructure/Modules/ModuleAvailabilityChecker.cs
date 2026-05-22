namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure.Modules;

public static class ModuleAvailabilityChecker
{
    public static bool IsModuleEnabled(this IConfiguration configuration, string module)
    {
        var featureManagement = configuration.GetSection("FeatureManagement");
        return featureManagement.GetValue<bool>(module);
    }

    public static bool IsModuleEnabled(this IApplicationBuilder app, string module)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        return configuration.IsModuleEnabled(module);
    }
}
