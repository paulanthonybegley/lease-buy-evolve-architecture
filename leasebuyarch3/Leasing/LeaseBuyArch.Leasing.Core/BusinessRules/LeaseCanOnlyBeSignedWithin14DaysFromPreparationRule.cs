using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.BusinessRules;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Core.BusinessRules;

internal sealed class LeaseCanOnlyBeSignedWithin14DaysFromPreparation : IBusinessRule
{
    private readonly DateTimeOffset _preparedAt;
    private readonly DateTimeOffset _signedAt;

    internal LeaseCanOnlyBeSignedWithin14DaysFromPreparation(DateTimeOffset preparedAt, DateTimeOffset signedAt)
    { _preparedAt = preparedAt; _signedAt = signedAt; }
    public bool IsMet() => (_signedAt.Date - _preparedAt.Date) <= TimeSpan.FromDays(14);
    public string Error => "Lease can not be signed because more than 14 days have passed from the lease preparation";
}
