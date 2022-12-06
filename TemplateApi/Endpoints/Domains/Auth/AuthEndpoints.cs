using Auth.Domain.Hashing.Abstract;
using Auth.Domain.Hashing.Concrete;
using Auth.Domain.Jwt;
using Auth.Domain.Models;
using Auth.Domain.OptionsSetup;
using Auth.Domain.Repositories.Abstract;
using Auth.Domain.Services.Abstract;
using Auth.Domain.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TemplateApi.Commons.Endpoints.Abstract;
using TemplateApi.Persistence.Concrete;

namespace TemplateApi.Endpoints.Domains.Auth
{
    public class AuthEndpoints : IEndpoints
    {
        private const string EndpointName = "Auth";
        private const string ContentType = "application/json";
        public static void AddServices(IServiceCollection services)
        {
            services.TryAddScoped<IPasswordHasher, PasswordHasherRfc2898>();
            services.TryAddScoped<IAuthRepository, AuthRepository>();
            services.TryAddScoped<IAuthService, AuthService>();
            services.TryAddScoped<IJwtProvider, JwtProvider>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();
            services.AddAuthorization();
        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost($"{EndpointName}", AuthRequests.SignIn)
                .WithName("SignIn")
                .Accepts<SignInDto>(ContentType)
                .Produces<string>(200)
                .Produces(400)
                .WithTags(EndpointName);
        }
    }
}
