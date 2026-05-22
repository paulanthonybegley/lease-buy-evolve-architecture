using EvolutionaryArchitecture.LeaseBuyArch.Common.BusinessRulesEngine;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.PrepareLease.BusinessRules;

internal sealed class CustomerCreditScoreMustBeHighEnoughRule : IBusinessRule
{
    private readonly int _creditScore;
    private readonly int _minimum;

    internal CustomerCreditScoreMustBeHighEnoughRule(int creditScore, int minimum)
    {
        _creditScore = creditScore;
        _minimum = minimum;
    }

    public bool IsMet() => _creditScore >= _minimum;

    public string Error => $"Customer credit score {_creditScore} is below the minimum requirement of {_minimum}";
}
