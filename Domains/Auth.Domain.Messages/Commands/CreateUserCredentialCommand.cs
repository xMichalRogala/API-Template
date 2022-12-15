using TemplateApi.CQRS.Commands.Abstract;

namespace Auth.Domain.Messages.Commands
{
    public class CreateUserCredentialCommand : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public CreateUserCredentialCommand(string login, string password)
        {
            Login = login;
            Password= password;
        }
    }
}
