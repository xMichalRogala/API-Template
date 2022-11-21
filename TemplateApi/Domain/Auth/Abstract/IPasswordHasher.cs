using TemplateApi.Domain.Auth.Models;

namespace TemplateApi.Domain.Auth.Abstract
{
    public interface IPasswordHasher
    {
        PasswordHashResult Hash(string password);
        bool Check(string key, string salt, int iterations, string password);
    }
}
