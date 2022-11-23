using TemplateApi.Domain.Auth.DAL.Abstract;
using TemplateApi.Domain.Auth.Entities;
using TemplateApi.Domain.Auth.Hashing.Abstract;
using TemplateApi.Domain.Auth.Services.Abstract;

namespace TemplateApi.Domain.Auth.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task SaveUserCredentials(string login, string password)
        {
            var hashResult = _passwordHasher.Hash(password);

            await _authRepository.Add(new UserCredential
            {
                Iterations = hashResult.Iterations,
                Login = login,
                Salt = hashResult.Salt,
                PasswordBytes = hashResult.Key
            });
        }

        public async Task<bool> ValidatePassword(string login, string password)
        {
            var userCredential = await _authRepository.Find(x => x.Login == login)
                ?? throw new ArgumentNullException($"There are any userCredentials for login: {login}");

            return _passwordHasher.Check(userCredential.PasswordBytes, userCredential.Salt, userCredential.Iterations, password);
        }
    }
}
