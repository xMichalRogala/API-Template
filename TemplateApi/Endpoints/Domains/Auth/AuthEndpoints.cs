using Auth.Domain.Authorization;
using Auth.Domain.Authorization.Abstract;
using Auth.Domain.Hashing.Abstract;
using Auth.Domain.Hashing.Concrete;
using Auth.Domain.Jwt;
using Auth.Domain.OptionsSetup;
using Auth.Domain.Repositories.Abstract;
using Auth.Domain.Repositories.Concrete;
using Auth.Domain.Schemas.Models;
using Auth.Domain.Services.Abstract;
using Auth.Domain.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TemplateApi.Commons.Endpoints.Abstract;

namespace TemplateApi.Endpoints.Domains.Auth
{
    public class AuthEndpoints : IEndpoints
    {
        private const string EndpointName = "Auth";
        private const string ContentType = "application/json";
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped<IPasswordHasher, PasswordHasherRfc2898>();
            services.TryAddScoped<IAuthRepository, AuthRepository>();
            services.TryAddScoped<IAuthService, AuthService>();
            services.TryAddScoped<IJwtProvider, JwtProvider>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Auth:Jwt:Issuer"],
                    ValidAudience = configuration["Auth:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Jwt:SecretKey"]))
                }; 
            });
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();
            services.AddAuthorization();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddScoped<IPermissionService, PermissionService>();
        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost($"{EndpointName}", AuthRequests.SignIn)
                .WithName("SignIn")
                .Accepts<SignInDto>(ContentType)
                .Produces<string>(200)
                .Produces(400);
        }
    }
}
