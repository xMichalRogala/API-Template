using Microsoft.Extensions.Hosting;
using TemplateApi.CQRS.Events.Concrete;

namespace TemplateApi.CQRS.BackgroundServices
{
    public sealed class EventQueueManagerHostedService : BackgroundService
    {
        private readonly EventQueueManager eventQueueManager;

        public EventQueueManagerHostedService(EventQueueManager eventQueueManager)
        {
            this.eventQueueManager = eventQueueManager;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await eventQueueManager.StartWorkAsync(stoppingToken);
        }
    }
}
