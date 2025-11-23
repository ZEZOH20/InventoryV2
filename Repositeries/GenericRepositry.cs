using InventoryV2.Data.DbContexts;
using InventoryV2.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace InventoryV2.Repositeries
{
    public class GenericRepositry<TEntity> : IGenericRepositry<TEntity> where TEntity : class
    {
        readonly SqlDbContext _context;
        readonly DbSet<TEntity> _dbSet;
        public GenericRepositry(SqlDbContext context) {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public Task<bool> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        //public Task<bool> AddAsync(TEntity entity)
        //    => await _dbSet.AddAsync(entity);

        public Task<bool> DeleteAsync(int id)  
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
