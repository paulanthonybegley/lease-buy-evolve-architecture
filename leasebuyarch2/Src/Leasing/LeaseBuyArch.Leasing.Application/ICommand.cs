using MediatR;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application;

public interface ICommand<out TResult> : IRequest<TResult>
{
}

public interface ICommand : IRequest
{
}
