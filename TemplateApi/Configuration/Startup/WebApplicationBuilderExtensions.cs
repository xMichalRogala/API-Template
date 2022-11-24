using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TemplateApi.Common.Data;
using TemplateApi.Commons.Data.Repository;
using TemplateApi.Commons.Endpoints.Abstract;
using TemplateApi.Domain.Auth.DAL.Abstract;
using TemplateApi.Domain.Auth.DAL.Concrete;
using TemplateApi.Domain.Auth.Hashing.Abstract;
using TemplateApi.Domain.Auth.Hashing.Concrete;
using TemplateApi.Domain.Auth.Services.Abstract;
using TemplateApi.Domain.Auth.Services.Concrete;
using TemplateApi.Domain.Core.DAL.Abstract;
using TemplateApi.Domain.Core.DAL.Concrete;

namespace TemplateApi.Configuration.Startup
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IPasswordHasher, PasswordHasherRfc2898>();
            builder.Services.TryAddScoped<IAuthRepository, AuthRepository>();
            builder.Services.TryAddScoped<IAuthService, AuthService>();
            builder.Services.TryAddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();

            AddServicesFromEndpoints(builder.Services);

            return builder;
        }

        public static WebApplicationBuilder AddAuthDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection("Auth:ConnectionString")?.Value);

                if (builder.Environment.IsDevelopment())
                    EnableEfDebugOptions(options);
            });

            return builder;
        }

        public static WebApplicationBuilder AddApplicationDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));

                if (builder.Environment.IsDevelopment())
                    EnableEfDebugOptions(options);
            });

            return builder;
        }

        private static void EnableEfDebugOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        private static void AddServicesFromEndpoints(IServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var endpointTypes = assembly.GetTypes()
                    .Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEndpoints).IsAssignableFrom(x));

                foreach (var endpointType in endpointTypes)
                {
                    endpointType.GetMethod(nameof(IEndpoints.AddServices))
                        !.Invoke(null, new object[] { services });
                }
            }
        }
    }
}
