using Core.Domain.Models;
using TemplateApi.Commons.Endpoints.Abstract;

namespace TemplateApi.Endpoints.Domains.Core
{
    internal sealed class UserEndpoints : IEndpoints
    {
        public static void AddServices(IServiceCollection services)
        {

        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("users", UserRequests.Create)
                .WithName("CreateUser")
                .Accepts<UserDto>("application/json");
        }
    }
}
