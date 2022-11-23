using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using TemplateApi.Domain.Auth.Abstract;
using TemplateApi.Domain.Auth.Models;

namespace TemplateApi.Domain.Auth.Concrete
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

        public bool Check(string key, string salt, int iterations, string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(_keySize);

                return keyToCheck.SequenceEqual(Convert.FromBase64String(key));
            }
        }

        public PasswordHashResult Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, _saltSize, _options.Iterations, HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_keySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return new PasswordHashResult(salt, key, _options.Iterations);
            }
        }
    }
}
