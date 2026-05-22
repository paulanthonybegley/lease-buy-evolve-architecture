using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.CompletePurchase;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.GetAllPurchases;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.OfferPurchase;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing;

internal static class PurchasingEndpoints
{
    internal static void MapPurchasing(this IEndpointRouteBuilder app)
    {
        app.MapGetAllPurchases();
        app.MapCompletePurchase();
        app.MapOfferPurchase();
    }
}
