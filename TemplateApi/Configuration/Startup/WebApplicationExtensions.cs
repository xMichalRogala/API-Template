using TemplateApi.Commons.Endpoints.Abstract;

namespace TemplateApi.Configuration.Startup
{
    public static class WebApplicationExtensions
    {
        public static WebApplication AddCustomMiddlewares(this WebApplication application)
        {

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
    }
}
