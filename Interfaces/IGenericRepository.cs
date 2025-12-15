using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace InventoryV2.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

    }
}
