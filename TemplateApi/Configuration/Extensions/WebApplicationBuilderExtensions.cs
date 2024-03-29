﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TemplateApi.Application.Abstract;
using TemplateApi.Commons.Data.Repository;
using TemplateApi.Commons.Endpoints.Abstract;
using TemplateApi.Persistence;
using TemplateApi.Persistence.DbContexts.Application;
using TemplateApi.Persistence.DbContexts.Auth;
using TemplateApi.Persistence.Interceptors;

namespace TemplateApi.Configuration.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();

            AddServicesFromEndpoints(builder);

            return builder;
        }

        public static WebApplicationBuilder AddAuthDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection("Auth:ConnectionString")?.Value, 
                    b => b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));

                if (builder.Environment.IsDevelopment())
                    EnableEfDebugOptions(options);
            });

            return builder;
        }

        public static WebApplicationBuilder AddApplicationDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<AddEventsToQueueEventManagerInterceptor>();

            builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"), 
                    b => b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name))
                .AddInterceptors(sp.GetService<AddEventsToQueueEventManagerInterceptor>());

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

        private static void AddServicesFromEndpoints(WebApplicationBuilder builder)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var endpointTypes = assembly.GetTypes()
                    .Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEndpoints).IsAssignableFrom(x));

                foreach (var endpointType in endpointTypes)
                {
                    endpointType.GetMethod(nameof(IEndpoints.AddServices))
                        !.Invoke(null, new object[] { builder.Services, builder.Configuration });
                }
            }
        }
    }
}
