using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.BusinessRules;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core.BusinessRules;

internal sealed class MileageMustNotExceedMaxLimitRule : IBusinessRule
{
    private readonly int _annualMileageLimit;
    private readonly int _maxAnnualMileage;
    internal MileageMustNotExceedMaxLimitRule(int annualMileageLimit, int maxAnnualMileage)
    { _annualMileageLimit = annualMileageLimit; _maxAnnualMileage = maxAnnualMileage; }
    public bool IsMet() => _annualMileageLimit <= _maxAnnualMileage;
    public string Error => $"Annual mileage limit {_annualMileageLimit} exceeds maximum allowed {_maxAnnualMileage}";
}
