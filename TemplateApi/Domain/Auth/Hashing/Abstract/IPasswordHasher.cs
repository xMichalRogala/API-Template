using TemplateApi.Domain.Auth.Models;

namespace TemplateApi.Domain.Auth.Hashing.Abstract
{
    public interface IPasswordHasher
    {
        PasswordHashResult Hash(string password);
        bool Check(byte[] key, string salt, int iterations, string password);
    }
}
