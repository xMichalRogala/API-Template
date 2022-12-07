using Auth.Domain.Entities;
using Auth.Domain.Hashing.Abstract;
using Auth.Domain.Jwt;
using Auth.Domain.Repositories.Abstract;
using Auth.Domain.Services.Abstract;

namespace Auth.Domain.Services.Concrete
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

        public async Task<bool> RemoveUserCredentials(string login, CancellationToken cancellationToken = default)
        {
            var userCredential = await _authRepository.Find(x => x.Login.ToLower() == login.ToLower(), cancellationToken);

            if (userCredential == null)
                return false;

            _authRepository.Delete(userCredential);

            return await _authRepository.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task SaveUserCredentials(string login, string password, CancellationToken cancellationToken = default)
        {
            var hashResult = _passwordHasher.Hash(password);

            await _authRepository.Add(new UserCredential
            {
                Iterations = hashResult.Iterations,
                Login = login,
                Salt = hashResult.Salt,
                PasswordBytes = hashResult.Key
            }, cancellationToken);

            await _authRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> ValidatePassword(string login, string password, CancellationToken cancellationToken = default)
        {
            var userCredential = await _authRepository.Find(x => x.Login == login, cancellationToken)
                ?? throw new ArgumentNullException($"There are any userCredentials for login: {login}");

            if (!_passwordHasher.Check(userCredential.PasswordBytes, userCredential.Salt, userCredential.Iterations, password))
                return string.Empty;

            return _jwtProvider.GenerateToken(userCredential);
        }
    }
}
