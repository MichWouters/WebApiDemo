using System.Linq.Expressions;

namespace WebAPIDemo.Repositories
{
    // TEntity is een placeholder voor het uiteindelijke model (bv. Laptop of Product)
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // C - Create
        void Add(TEntity entity);

        // R - Read (All)
        Task<IEnumerable<TEntity>> GetAllAsync();

        // R - Read (Single)
        Task<TEntity?> GetByIdAsync(int id);

        // U - Update
        void Update(TEntity entity);

        // D - Delete
        void Delete(TEntity entity);
    }
}