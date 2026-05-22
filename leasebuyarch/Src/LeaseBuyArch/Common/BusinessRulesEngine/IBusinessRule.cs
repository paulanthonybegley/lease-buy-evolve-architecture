namespace EvolutionaryArchitecture.LeaseBuyArch.Common.BusinessRulesEngine;

internal interface IBusinessRule
{
    bool IsMet();
    string Error { get; }
}
