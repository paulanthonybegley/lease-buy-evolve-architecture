using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.BusinessRules;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core.BusinessRules;

internal sealed class PreviousLeaseMustBeSettledRule : IBusinessRule
{
    private readonly bool? _settled;
    internal PreviousLeaseMustBeSettledRule(bool? settled) => _settled = settled;
    public bool IsMet() => _settled is true or null;
    public string Error => "Previous lease must be settled by the customer";
}
