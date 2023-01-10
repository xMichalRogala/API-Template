using Auth.Domain.Messages.Commands;
using Core.Domain.Messages.Events;
using TemplateApi.CQRS.Commands.Abstract;
using TemplateApi.CQRS.Events.Abstract;

namespace Auth.Domain.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreated>
    {
        private readonly ICommandDispatcher commandDispatcher;

        public UserCreatedEventHandler(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public Task HandleAsync(UserCreated @event, CancellationToken cancellationToken)
        {
            return this.commandDispatcher.DispatchAsync(new CreateUserCredentialCommand(@event.Login, @event.Password), cancellationToken);
        }
    }
}
