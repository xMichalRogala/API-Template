using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Commons.Assemblies;
using TemplateApi.CQRS.BackgroundServices;
using TemplateApi.CQRS.Commands.Abstract;
using TemplateApi.CQRS.Commands.Concrete;
using TemplateApi.CQRS.Commands.Models;
using TemplateApi.CQRS.Events.Abstract;
using TemplateApi.CQRS.Events.Concrete;
using TemplateApi.CQRS.Events.Models;
using TemplateApi.CQRS.Queries.Abstract;
using TemplateApi.CQRS.Queries.Concrete;

namespace TemplateApi.CQRS.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private static IServiceCollection AddHandlersToServices(this IServiceCollection services, Type handlerType)
        {
            var assemblies = LoadAllAssemblies.Get(); //todo create cache assemblies contaieer as singleton

            foreach (var assembly in assemblies)
            {
                var commandHandlers = assembly.GetTypes().Where(t => t.GetInterfaces().Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == handlerType));

                foreach (var handler in commandHandlers)
                {
                    services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType), handler);
                }
            }

            return services;
        }

        private static IServiceCollection AddCommands(this IServiceCollection services, Action<CommandOptions> opt)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddHandlersToServices(typeof(ICommandHandler<>));
            services.AddOptions<CommandOptions>().Configure(opt);

            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.AddHandlersToServices(typeof(IQueryHandler<,>));

            return services;
        }

        private static IServiceCollection AddEvents(this IServiceCollection services, Action<EventOptions> opt)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddHandlersToServices(typeof(IEventHandler<>));
            services.AddSingleton<EventQueueManager>();
            services.AddOptions<EventOptions>().Configure(opt);
            services.AddHostedService<EventQueueManagerHostedService>();

            return services;
        }

        public static IServiceCollection AddCustomCqrs(
            this IServiceCollection services, 
            Action<CommandOptions> commandOptions, 
            Action<EventOptions> eventOptions)
        {
            services.AddCommands(commandOptions);
            services.AddQueries();
            services.AddEvents(eventOptions);

            return services;
        }
    }
}
