using TemplateApi.Domain.Auth.Entities;

namespace TemplateApi.Domain.Auth.Jwt
{
    public interface IJwtProvider
    {
        public string GenerateToken(UserCredential userCredential);
    }
}
