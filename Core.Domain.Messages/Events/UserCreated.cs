using TemplateApi.CQRS.Events.Abstract;

namespace Core.Domain.Messages.Events
{
    public class UserCreated : IEvent
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
