using System.Linq.Expressions;

namespace TemplateApi.Commons.Data.Repository
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T> GetById(TKey id, CancellationToken cancellationToken = default);
        Task Add(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);
    }
}
