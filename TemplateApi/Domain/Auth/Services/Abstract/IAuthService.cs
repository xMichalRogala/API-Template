namespace TemplateApi.Domain.Auth.Services.Abstract
{
    public interface IAuthService
    {
        Task SaveUserCredentials(string login, string password, CancellationToken cancellationToken = default);

        Task<string> ValidatePassword(string login, string password, CancellationToken cancellationToken = default);

        Task<bool> RemoveUserCredentials(string login, CancellationToken cancellationToken = default);
    }
}
