namespace TemplateApi.CQRS.Commands.Abstract
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command, CancellationToken cancellationToken = default) where T : ICommand;
    }
}
