using MediatR;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Infrastructure;

internal sealed class LeasingModule : ILeasingModule
{
    private readonly IMediator _mediator;

    public LeasingModule(IMediator mediator) => _mediator = mediator;

    public Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default) =>
        _mediator.Send(command, cancellationToken);

    public Task ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default) =>
        _mediator.Send(command, cancellationToken);
}
