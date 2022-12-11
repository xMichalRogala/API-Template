using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateApi.Commons.Endpoints.Abstract
{
    public interface IEndpoints
    {
        static abstract void DefineEndpoints(IEndpointRouteBuilder app);
        static abstract void AddServices(IServiceCollection services, IConfiguration configuration = default);
    }
}
