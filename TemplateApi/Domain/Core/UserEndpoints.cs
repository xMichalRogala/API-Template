using TemplateApi.Commons.Endpoints.Abstract;
using TemplateApi.Domain.Core.Models;

namespace TemplateApi.Domain.Core
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
