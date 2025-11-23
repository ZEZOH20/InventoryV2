using System.Runtime.CompilerServices;

namespace InventoryV2.Interfaces
{
    public interface IGenericRepositry<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(int id);
        Task<bool> DeleteAsync(int id);

    }
}
