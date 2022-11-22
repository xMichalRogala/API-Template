using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TemplateApi.Domain.Auth;
using TemplateApi.Domain.Auth.Abstract;
using TemplateApi.Domain.Auth.Concrete;

namespace TemplateApi.Configuration.Startup
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IPasswordHasher, PasswordHasherRfc2898>();

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
