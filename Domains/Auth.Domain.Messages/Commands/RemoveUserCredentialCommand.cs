using TemplateApi.CQRS.Commands.Abstract;

namespace Auth.Domain.Messages.Commands
{
    public class RemoveUserCredentialCommand : ICommand
    {
        public string Login { get; set; }

        public RemoveUserCredentialCommand(string login)
        {
            Login = login;
        }
    }
}
