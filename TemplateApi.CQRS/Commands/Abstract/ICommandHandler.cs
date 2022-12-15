namespace TemplateApi.CQRS.Commands.Abstract
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
