using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TemplateApi.Domain.Auth.Models;
using TemplateApi.Domain.Auth.Services.Abstract;

namespace TemplateApi.Domain.Auth
{
    public class AuthRequests
    {
        public async static Task<IResult> SignIn(SignInDto signInDto, IAuthService authService)
        {
            var result = await authService.ValidatePassword(signInDto.Login, signInDto.Password);
            if (result.IsNullOrEmpty())
                return Results.BadRequest();

            return Results.Ok(result);
        }
     }
}
