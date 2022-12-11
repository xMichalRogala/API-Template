using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Auth.Domain.Hashing.Abstract;
using Auth.Domain.Schemas.Models;

namespace Auth.Domain.Hashing.Concrete
{
    public sealed class PasswordHasherRfc2898 : IPasswordHasher
    {
        private const int _saltSize = 16;
        private const int _keySize = 32;
        private HashingOptions _options { get; }

        public PasswordHasherRfc2898(IOptions<HashingOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(byte[] key, string salt, int iterations, string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(_keySize);

                return keyToCheck.SequenceEqual(key);
            }
        }

        public PasswordHashResult Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, _saltSize, _options.Iterations, HashAlgorithmName.SHA256))
            {
                var key = algorithm.GetBytes(_keySize);
                var salt = Convert.ToBase64String(algorithm.Salt);

                return new PasswordHashResult(salt, key, _options.Iterations);
            }
        }
    }
}
