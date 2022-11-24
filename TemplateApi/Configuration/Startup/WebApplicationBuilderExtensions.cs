using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TemplateApi.Common.Data;
using TemplateApi.Commons.Data.Repository;
using TemplateApi.Domain.Auth.DAL.Abstract;
using TemplateApi.Domain.Auth.DAL.Concrete;
using TemplateApi.Domain.Auth.Hashing.Abstract;
using TemplateApi.Domain.Auth.Hashing.Concrete;

namespace TemplateApi.Configuration.Startup
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IPasswordHasher, PasswordHasherRfc2898>();
            builder.Services.TryAddScoped<IAuthRepository, AuthRepository>();
            builder.Services.TryAddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

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
    }
}
