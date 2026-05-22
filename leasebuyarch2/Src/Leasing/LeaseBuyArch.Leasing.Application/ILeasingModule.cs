namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application;

public interface ILeasingModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    Task ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default);
}
