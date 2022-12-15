using Auth.Domain.Messages.Commands;
using Auth.Domain.Services.Abstract;
using TemplateApi.CQRS.Commands.Abstract;

namespace Auth.Domain.CommandHandlers
{
    public class UserCredentialCommandHandler : ICommandHandler<CreateUserCredentialCommand>, ICommandHandler<RemoveUserCredentialCommand>
    {
        private readonly IAuthService _authService;
        public UserCredentialCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task ExecuteAsync(CreateUserCredentialCommand command, CancellationToken cancellationToken = default)
        {
            return _authService.SaveUserCredentials(command.Login, command.Password, cancellationToken);
        }

        public Task ExecuteAsync(RemoveUserCredentialCommand command, CancellationToken cancellationToken = default)
        {
            return _authService.RemoveUserCredentials(command.Login, cancellationToken);
        }
    }
}
