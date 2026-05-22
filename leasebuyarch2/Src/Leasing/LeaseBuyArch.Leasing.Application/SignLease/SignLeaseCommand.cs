using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.SignLease;

public sealed record SignLeaseCommand(Guid Id) : ICommand;
