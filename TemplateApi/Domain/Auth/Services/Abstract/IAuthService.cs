namespace TemplateApi.Domain.Auth.Services.Abstract
{
    public interface IAuthService
    {
        Task SaveUserCredentials(string login, string password);

        Task<bool> ValidatePassword(string login, string password);

        Task<bool> RemoveUserCredentials(string login);
    }
}
