using System.Linq.Expressions;

namespace TemplateApi.Commons.Data.Repository
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Find(Func<T, bool> predicate);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);
        Task<T> GetById(TKey id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
