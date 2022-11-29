using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TemplateApi.Domain.Auth.Entities;

namespace TemplateApi.Domain.Auth.Jwt
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string GenerateToken(UserCredential userCredential)
        {
            var claims = new Claim[] 
            { 
                new(JwtRegisteredClaimNames.Name, userCredential.Login),
                new(JwtRegisteredClaimNames.Sub, userCredential.Id.ToString()),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(_options.ExpireTokenTimeInMinutes),
                signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
