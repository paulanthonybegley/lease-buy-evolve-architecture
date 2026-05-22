using EvolutionaryArchitecture.LeaseBuyArch.Common.BusinessRulesEngine;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.PrepareLease.BusinessRules;

internal sealed class PreviousLeaseMustBeSettledRule : IBusinessRule
{
    private readonly bool? _settled;

    internal PreviousLeaseMustBeSettledRule(bool? settled) => _settled = settled;

    public bool IsMet() => _settled is true or null;

    public string Error => "Previous lease must be settled by the customer";
}
