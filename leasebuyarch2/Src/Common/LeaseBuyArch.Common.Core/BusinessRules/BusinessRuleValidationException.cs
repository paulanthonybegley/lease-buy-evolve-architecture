namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Core.BusinessRules;

public class BusinessRuleValidationException : InvalidOperationException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}
