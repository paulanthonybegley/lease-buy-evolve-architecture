using EvolutionaryArchitecture.LeaseBuyArch.Common.BusinessRulesEngine;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.SignLease.BusinessRules;

internal sealed class LeaseCanOnlyBeSignedWithin14DaysFromPreparation : IBusinessRule
{
    private readonly DateTimeOffset _preparedAt;
    private readonly DateTimeOffset _signedAt;

    internal LeaseCanOnlyBeSignedWithin14DaysFromPreparation(DateTimeOffset preparedAt, DateTimeOffset signedAt)
    {
        _preparedAt = preparedAt;
        _signedAt = signedAt;
    }

    public bool IsMet()
    {
        var timeDifference = _signedAt.Date - _preparedAt.Date;
        return timeDifference <= TimeSpan.FromDays(14);
    }

    public string Error =>
        "Lease can not be signed because more than 14 days have passed from the lease preparation";
}
