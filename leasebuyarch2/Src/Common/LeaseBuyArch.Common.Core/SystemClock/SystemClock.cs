namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;

public sealed class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
