using Core.Domain.Schemas.Models;
using TemplateApi.Commons.Endpoints.Abstract;

namespace TemplateApi.Endpoints.Domains.Core
{
    internal sealed class UserEndpoints : IEndpoints
    {
        private const string EndpointName = "Users";
        private const string ContentType = "application/json";
        public static void AddServices(IServiceCollection services)
        {

        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost(EndpointName, UserRequests.Create)
                .WithName("CreateUser")
                .Accepts<UserDto>(ContentType);
        }
    }
}
