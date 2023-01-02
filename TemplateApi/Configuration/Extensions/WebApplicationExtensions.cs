using TemplateApi.Commons.Endpoints.Abstract;
using TemplateApi.CQRS.Events.Concrete;
using TemplateApi.Middlewares;

namespace TemplateApi.Configuration.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication AddCustomMiddlewares(this WebApplication application)
        {
            application.UseMiddleware<OperationCanceledMiddleware>();

            return application;
        }

        public static void AddCustomEndpoints(this WebApplication application)
        {
            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var endpointTypes = assembly.GetTypes()
                    .Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEndpoints).IsAssignableFrom(x));

                foreach(var endpointType in endpointTypes)
                {
                    endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))
                        !.Invoke(null, new object[] { application });
                }
            }
        }

        public static WebApplication AddCustomBackgroundTasks(this WebApplication application)
        {
            var eventQueueManager = application.Services.GetRequiredService<EventQueueManager>();

            eventQueueManager.StartWorkAsync();

            return application;
        }
    }
}
