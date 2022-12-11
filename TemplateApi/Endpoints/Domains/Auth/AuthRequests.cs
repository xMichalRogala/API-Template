using Auth.Domain.Schemas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Auth.Domain.Services.Abstract;

namespace TemplateApi.Endpoints.Domains.Auth
{
    public abstract class AuthRequests
    {
        [AllowAnonymous]
        public static async Task<IResult> SignIn([FromBody] SignInDto signInDto,
            IAuthService authService,
            CancellationToken cancellationToken)
        {
            var result = await authService.ValidatePassword(signInDto.Login, signInDto.Password, cancellationToken);

            if (result.IsNullOrEmpty())
                return Results.BadRequest();

            return Results.Ok(result);
        }
    }
}
