namespace TemplateApi.CQRS.Queries.Abstract
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);
    }
}
