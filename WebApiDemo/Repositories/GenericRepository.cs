using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;

namespace WebAPIDemo.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        // _context wordt protected zodat overervende klassen deze ook kunnen gebruiken.
        protected readonly WebAPIDemoContext _context;

        public GenericRepository(WebAPIDemoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // Set<TEntity> pakt automatisch de juiste tabel (bv. Laptops of Producten), gebaseerd op het type dat TEntity zal vervangen.
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            // Attach zorgt ervoor dat EF de entiteit gaat tracken als 'Modified'
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}