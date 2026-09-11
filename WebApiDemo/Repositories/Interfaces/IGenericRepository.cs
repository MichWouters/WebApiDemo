using WebApiDemo.Models;

namespace WebApiDemo.Repositories;

// TEntity is een placeholder voor het uiteindelijke model (bv. Laptop of Product)
public interface IGenericRepository<TEntity> where TEntity : class, IModel
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

    // Controleer of een item in een tabel bestaat
    Task<bool> ExistsAsync(int id);

    // Haal de lijst met alle id's uit een tabel op
    Task<int[]> GetExistingIdsAsync(IEnumerable<int> ids);
}