using System.Linq.Expressions;

namespace Bookify.Repository.IRepository
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            params Expression<Func<T, object>>[] includeProperties
        );
        Task<T?> GetByIdAsync(TKey id, bool tracked = true); 
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TKey id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
