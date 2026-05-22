namespace EvolutionaryArchitecture.LeaseBuyArch.Common.BusinessRulesEngine;

internal class BusinessRuleValidationException : InvalidOperationException
{
    internal BusinessRuleValidationException(string message) : base(message)
    {
    }
}
