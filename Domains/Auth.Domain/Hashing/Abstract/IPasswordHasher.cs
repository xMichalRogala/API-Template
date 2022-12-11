

using Auth.Domain.Schemas.Models;

namespace Auth.Domain.Hashing.Abstract
{
    public interface IPasswordHasher
    {
        PasswordHashResult Hash(string password);
        bool Check(byte[] key, string salt, int iterations, string password);
    }
}
