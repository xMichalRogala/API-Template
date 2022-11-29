using Microsoft.Extensions.Options;
using TemplateApi.Domain.Auth.Jwt;

namespace TemplateApi.Domain.Auth.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;
        private const string SectionName = "Auth:Jwt";

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
