using EvolutionaryArchitecture.LeaseBuyArch.Leasing.PrepareLease;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.SignLease;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing;

internal static class LeasingEndpoints
{
    internal static void MapLeasing(this IEndpointRouteBuilder app)
    {
        app.MapPrepareLease();
        app.MapSignLease();
    }
}
