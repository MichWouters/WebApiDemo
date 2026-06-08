using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories
{
    public class LaptopRepository : GenericRepository<Laptop>, ILaptopRepository
    {
        // base(context) stuurt de binnengekomen context door naar de GenericRepository
        public LaptopRepository(WebAPIDemoContext context) : base(context) { }

        // We implementeren enkel de specifieke methode
        public async Task<IEnumerable<Laptop>> GetLaptopsByMerkAsync(string merk)
        {
            // Dankzij 'protected' in GenericRepository kunnen we hier bij _context
            return await _context.Laptops
                .Where(x => x.Merk.ToLower() == merk.ToLower())
                .ToListAsync();
        }
    }
}