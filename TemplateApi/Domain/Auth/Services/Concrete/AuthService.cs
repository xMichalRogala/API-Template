using TemplateApi.Domain.Auth.DAL.Abstract;
using TemplateApi.Domain.Auth.Entities;
using TemplateApi.Domain.Auth.Hashing.Abstract;
using TemplateApi.Domain.Auth.Jwt;
using TemplateApi.Domain.Auth.Services.Abstract;

namespace TemplateApi.Domain.Auth.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<bool> RemoveUserCredentials(string login)
        {
            var userCredential = await _authRepository.Find(x => x.Login.ToLower() == login.ToLower());

            if (userCredential == null)
                return false;

            _authRepository.Delete(userCredential);

            return await _authRepository.SaveChangesAsync() > 0;
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

            await _authRepository.SaveChangesAsync();
        }

        public async Task<string> ValidatePassword(string login, string password)
        {
            var userCredential = await _authRepository.Find(x => x.Login == login)
                ?? throw new ArgumentNullException($"There are any userCredentials for login: {login}");

            if (!_passwordHasher.Check(userCredential.PasswordBytes, userCredential.Salt, userCredential.Iterations, password))
                return string.Empty;

            return _jwtProvider.GenerateToken(userCredential);
        }
    }
}
