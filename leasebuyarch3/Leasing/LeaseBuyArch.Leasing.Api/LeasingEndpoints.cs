using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api.PrepareLease;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api.SignLease;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api;

internal static class LeasingEndpoints
{
    internal static void MapLeasing(this IEndpointRouteBuilder app)
    {
        app.MapPrepareLease();
        app.MapSignLease();
    }
}
