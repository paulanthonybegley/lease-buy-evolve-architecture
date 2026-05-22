using FluentValidation;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.PrepareLease;

internal sealed class PrepareLeaseRequestValidator : AbstractValidator<PrepareLeaseRequest>
{
    public PrepareLeaseRequestValidator()
    {
        RuleFor(request => request.CustomerId).NotEmpty();
        RuleFor(request => request.VehicleId).NotEmpty();
        RuleFor(request => request.VehicleMsrp).GreaterThan(0);
        RuleFor(request => request.ResidualPercentage).InclusiveBetween(1, 99);
        RuleFor(request => request.MoneyFactor).GreaterThan(0);
        RuleFor(request => request.TermMonths).InclusiveBetween(12, 60);
        RuleFor(request => request.AnnualMileageLimit).GreaterThan(0);
        RuleFor(request => request.CreditScore).InclusiveBetween(300, 850);
    }
}
