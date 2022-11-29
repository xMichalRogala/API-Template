using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TemplateApi.Commons.Endpoints.Abstract;
using TemplateApi.Domain.Auth.DAL.Abstract;
using TemplateApi.Domain.Auth.DAL.Concrete;
using TemplateApi.Domain.Auth.Hashing.Abstract;
using TemplateApi.Domain.Auth.Hashing.Concrete;
using TemplateApi.Domain.Auth.Models;
using TemplateApi.Domain.Auth.OptionsSetup;
using TemplateApi.Domain.Auth.Jwt;
using TemplateApi.Domain.Auth.Services.Abstract;
using TemplateApi.Domain.Auth.Services.Concrete;

namespace TemplateApi.Domain.Auth
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
