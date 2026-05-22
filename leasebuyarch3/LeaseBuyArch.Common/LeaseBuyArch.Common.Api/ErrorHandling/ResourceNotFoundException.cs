namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Api.ErrorHandling;

public sealed class ResourceNotFoundException : InvalidOperationException
{
    public ResourceNotFoundException(Guid id) : base($"Resource with '{id}' not found ")
    {
    }
}
