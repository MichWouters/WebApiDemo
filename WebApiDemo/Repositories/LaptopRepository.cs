using Microsoft.EntityFrameworkCore; // Nodig voor de asynchrone extensiemethodes
using WebApiDemo.Data;
using WebAPIDemo.Models;

namespace WebApiDemo.Repositories
{
    public class LaptopRepository : ILaptopRepository
    {
        private readonly WebAPIDemoContext _context;

        public LaptopRepository(WebAPIDemoContext context)
        {
            _context = context;
        }

        // C - Create: Voeg een nieuwe laptop toe
        public async Task<Laptop> CreateAsync(Laptop laptop)
        {
            _context.Laptops.Add(laptop);
            await _context.SaveChangesAsync();

            return laptop;
        }

        // R - Read (All): Haal alle laptops op
        public async Task<List<Laptop>> GetAllAsync()
        {
            return await _context.Laptops.ToListAsync();
        }

        // R - Read (Single): Zoek een specifieke laptop op basis van ID
        public async Task<Laptop?> GetByIdAsync(int id)
        {
            return await _context.Laptops.FindAsync(id);
        }

        // U - Update: Pas de gegevens aan
        public async Task UpdateAsync(int id, Laptop updatedLaptop)
        {
            Laptop? existingLaptop = await _context.Laptops.FindAsync(id);

            if (existingLaptop != null)
            {
                existingLaptop.Merk = updatedLaptop.Merk;
                existingLaptop.Processor = updatedLaptop.Processor;
                existingLaptop.RamInGB = updatedLaptop.RamInGB;
                existingLaptop.Prijs = updatedLaptop.Prijs;
                existingLaptop.GPU = updatedLaptop.GPU;

                await _context.SaveChangesAsync();
            }
        }

        // D - Delete: Verwijder een laptop
        public async Task DeleteAsync(int id)
        {
            Laptop? laptopToDelete = await _context.Laptops.FindAsync(id);

            if (laptopToDelete != null)
            {
                _context.Laptops.Remove(laptopToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}