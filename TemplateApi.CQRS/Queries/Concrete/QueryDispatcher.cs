using Microsoft.Extensions.DependencyInjection;
using TemplateApi.CQRS.Queries.Abstract;

namespace TemplateApi.CQRS.Queries.Concrete
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var commandHandlers = serviceProvider.GetServices<IQueryHandler<TQuery, TResult>>();

                if (commandHandlers.Count() >= 1)
                    throw new InvalidOperationException($"Query has more than 1 queryHandlers implementing query type: {typeof(TQuery)}");

                if (commandHandlers.Count() == 0)
                    throw new InvalidOperationException($"There is any queryHandler to proceed query of type: {typeof(TQuery)}");

                return commandHandlers.First().Handle(query, cancellationToken);
            }
        }
    }
}
