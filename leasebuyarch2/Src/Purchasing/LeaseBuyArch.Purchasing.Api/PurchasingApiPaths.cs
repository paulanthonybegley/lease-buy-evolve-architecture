using EvolutionaryArchitecture.LeaseBuyArch.Common.Api;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api;

internal static class PurchasingApiPaths
{
    private const string Root = $"{ApiPaths.Root}/purchasing";
    internal const string GetAll = Root;
    internal const string Complete = $"{Root}/{{id}}";
    internal const string Offer = $"{Root}/offer";
}
