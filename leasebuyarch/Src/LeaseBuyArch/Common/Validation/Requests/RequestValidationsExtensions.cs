using FluentValidation;

namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Validation.Requests;

internal static class RequestValidationsExtensions
{
    internal static IServiceCollection AddRequestsValidations(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<Program>(includeInternalTypes: true);
}
