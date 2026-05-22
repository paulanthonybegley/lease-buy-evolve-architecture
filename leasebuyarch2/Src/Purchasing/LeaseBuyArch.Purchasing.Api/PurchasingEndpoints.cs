using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api.GetAllPurchases;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api.CompletePurchase;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api.OfferPurchase;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api;

internal static class PurchasingEndpoints
{
    internal static void MapPurchasing(this IEndpointRouteBuilder app)
    {
        app.MapGetAllPurchases();
        app.MapCompletePurchase();
        app.MapOfferPurchase();
    }
}
