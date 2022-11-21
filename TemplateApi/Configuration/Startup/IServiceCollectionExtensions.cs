using TemplateApi.Domain.Auth.Abstract;
using TemplateApi.Domain.Auth.Concrete;

namespace TemplateApi.Configuration.Startup
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection sc)
        {
            sc.AddEndpointsApiExplorer();
            sc.AddSwaggerGen();

            sc.AddScoped<IPasswordHasher, PasswordHasherRfc2898>();

            return sc;
        }
    }
}
