namespace TemplateApi.CQRS.Commands.Abstract
{
    public interface ICommandHandler<TCommand> : ICommand
    {
        Task ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
