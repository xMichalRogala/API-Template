using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using TemplateApi.Domain.Auth.Abstract;
using TemplateApi.Domain.Auth.Models;

namespace TemplateApi.Domain.Auth.Concrete
{
    public sealed class PasswordHasherRfc2898 : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private HashingOptions Options { get; }

        public PasswordHasherRfc2898(IOptions<HashingOptions> options)
        {
        Options = options.Value;
        }

        public bool Check(string key, string salt, int iterations, string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                return keyToCheck.SequenceEqual(Convert.FromBase64String(key));
            }
        }

        public PasswordHashResult Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Options.Iterations, HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return new PasswordHashResult(salt, key, Options.Iterations);
            }
        }
    }
}
