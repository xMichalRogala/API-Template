namespace TemplateApi.CQRS.Queries.Abstract
{
    public interface IQueryDispatcher
    {
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>;
    }
}
