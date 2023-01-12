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

        public async Task DispatchAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IEvent
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                List<Task> tasks= new List<Task>();
                var runTimeEventType = @event.GetType();
                var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(runTimeEventType);

                var eventHandlers = serviceProvider.GetServices(eventHandlerType);

                foreach (var eventHandler in eventHandlers)
                {
                    if(eventHandler == null)
                    {
                        continue;
                    }

                    var method = eventHandler.GetType().GetMethod(nameof(IEventHandler<T>.HandleAsync));
                    if (method != null)
                    {
                        var result = method.Invoke(eventHandler, new object[] { @event, cancellationToken }) as Task;

                        if(result != null)
                        {
                            tasks.Add(result);
                        }
                    }
                }

                await Task.WhenAll(tasks);
            }
        }
    }
}
