using System.Linq.Expressions;
using InventoryV2.Data.DbContexts;
using InventoryV2.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace InventoryV2.Repositeries
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SqlDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SqlDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetQuery() => _dbSet.AsQueryable();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) 
            => await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) 
            => await _dbSet.AddRangeAsync(entities);

        public void Update(T entity) => _context.Update(entity);
        
        public void Delete(T entity) => _dbSet.Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
    }
}

