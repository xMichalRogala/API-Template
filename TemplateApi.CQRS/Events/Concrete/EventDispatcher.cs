using Microsoft.Extensions.DependencyInjection;
using TemplateApi.CQRS.Events.Abstract;

namespace TemplateApi.CQRS.Events.Concrete
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task DispatchAsync<T>(T @event, CancellationToken cancellationToken) where T : IEvent
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var eventHandlers = serviceProvider.GetServices<IEventHandler<T>>().ToList();

                await Task.WhenAll(eventHandlers.Select(async x => await x.HandleAsync(@event, cancellationToken)));
            }
        }
    }
}
