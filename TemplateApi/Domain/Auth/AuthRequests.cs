using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TemplateApi.Domain.Auth.Models;
using TemplateApi.Domain.Auth.Services.Abstract;

namespace TemplateApi.Domain.Auth
{
    public class AuthRequests
    {
        [AllowAnonymous]
        public async static Task<IResult> SignIn([FromBody] SignInDto signInDto, 
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
