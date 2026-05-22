namespace EvolutionaryArchitecture.LeaseBuyArch.Common.BusinessRulesEngine;

internal static class BusinessRuleValidator
{
    internal static void Validate(IBusinessRule rule)
    {
        if (!rule.IsMet())
        {
            throw new BusinessRuleValidationException(rule.Error);
        }
    }
}
