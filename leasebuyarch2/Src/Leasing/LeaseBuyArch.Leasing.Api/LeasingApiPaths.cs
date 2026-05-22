using EvolutionaryArchitecture.LeaseBuyArch.Common.Api;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api;

internal static class LeasingApiPaths
{
    private const string LeasingRootApi = $"{ApiPaths.Root}/leasing";
    internal const string Prepare = LeasingRootApi;
    internal const string Sign = $"{LeasingRootApi}/{{id}}";
}
