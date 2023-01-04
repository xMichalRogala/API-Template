namespace TemplateApi.CQRS.Events.Abstract
{
    public abstract class AggregateRoot
    {
        private List<IEvent> _events = new List<IEvent>();

        protected void Publish(IEvent @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<IEvent> GetEventsToPublish()
        {
            return _events;
        }
    }
}
