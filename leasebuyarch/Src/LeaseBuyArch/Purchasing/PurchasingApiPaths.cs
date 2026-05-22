namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing;

internal static class PurchasingApiPaths
{
    private const string PurchasingRootApi = $"{ApiPaths.Root}/purchasing";

    internal const string GetAll = PurchasingRootApi;
    internal const string Complete = $"{PurchasingRootApi}/{{id}}";
    internal const string Offer = $"{PurchasingRootApi}/offer";
}
