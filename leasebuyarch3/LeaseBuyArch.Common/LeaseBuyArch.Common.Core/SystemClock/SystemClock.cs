namespace EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;

internal sealed class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
