namespace EvolutionaryArchitecture.LeaseBuyArch;

internal record Module(string Value)
{
    internal static readonly Module Leasing = new("Leasing");
    internal static readonly Module Purchasing = new("Purchasing");
    internal static readonly Module Vehicles = new("Vehicles");
    internal static readonly Module Comparison = new("Comparison");

    public static implicit operator string(Module module) => module.Value;
}
