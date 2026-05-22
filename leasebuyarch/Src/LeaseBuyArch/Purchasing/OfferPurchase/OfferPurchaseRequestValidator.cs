using FluentValidation;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.OfferPurchase;

internal sealed class OfferPurchaseRequestValidator : AbstractValidator<OfferPurchaseRequest>
{
    public OfferPurchaseRequestValidator()
    {
        RuleFor(request => request.CustomerId).NotEmpty();
        RuleFor(request => request.VehicleId).NotEmpty();
        RuleFor(request => request.VehicleMsrp).GreaterThan(0);
        RuleFor(request => request.DownPayment).GreaterThanOrEqualTo(0);
        RuleFor(request => request.Apr).InclusiveBetween(0, 30);
        RuleFor(request => request.TermMonths).InclusiveBetween(12, 84);
        RuleFor(request => request.PreparedAt).NotEmpty();
    }
}
