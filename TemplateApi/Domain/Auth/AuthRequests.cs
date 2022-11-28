using Microsoft.AspNetCore.Mvc;
using TemplateApi.Domain.Auth.Models;
using TemplateApi.Domain.Auth.Services.Abstract;

namespace TemplateApi.Domain.Auth
{
    public class AuthRequests
    {
        public async static Task<IResult> SignIn(SignInDto signInDto, IAuthService authService)
        {
            if(!await authService.ValidatePassword(signInDto.Login, signInDto.Password))
                return Results.BadRequest();

            //create JWT token etc

            return Results.Ok();
        }
     }
}
