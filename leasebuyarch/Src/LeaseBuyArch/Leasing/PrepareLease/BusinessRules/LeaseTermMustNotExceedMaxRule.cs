using EvolutionaryArchitecture.LeaseBuyArch.Common.BusinessRulesEngine;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.PrepareLease.BusinessRules;

internal sealed class LeaseTermMustNotExceedMaxRule : IBusinessRule
{
    private readonly int _termMonths;
    private readonly int _maxTermMonths;

    internal LeaseTermMustNotExceedMaxRule(int termMonths, int maxTermMonths)
    {
        _termMonths = termMonths;
        _maxTermMonths = maxTermMonths;
    }

    public bool IsMet() => _termMonths <= _maxTermMonths;

    public string Error => $"Lease term {_termMonths} months exceeds maximum {_maxTermMonths} months";
}
