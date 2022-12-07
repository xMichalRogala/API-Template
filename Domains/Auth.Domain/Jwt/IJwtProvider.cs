using Auth.Domain.Entities;

namespace Auth.Domain.Jwt
{
    public interface IJwtProvider
    {
        public string GenerateToken(UserCredential userCredential);
    }
}
