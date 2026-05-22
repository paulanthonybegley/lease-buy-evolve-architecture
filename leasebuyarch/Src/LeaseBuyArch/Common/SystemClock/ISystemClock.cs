namespace EvolutionaryArchitecture.LeaseBuyArch.Common.SystemClock;

internal interface ISystemClock
{
    DateTimeOffset Now { get; }
}
