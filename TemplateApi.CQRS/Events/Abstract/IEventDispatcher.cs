namespace TemplateApi.CQRS.Events.Abstract
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T>(T @event, CancellationToken cancellationToken) where T : IEvent;
    }
}
