using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;
using WebAPIDemo.Models;

namespace WebApiDemo.Repositories
{
    public class BestellingRepository : IBestellingRepository
    {
        private readonly WebAPIDemoContext _context;

        public BestellingRepository(WebAPIDemoContext context)
        {
            _context = context;
        }

        public async Task<Bestelling> CreateAsync(Bestelling bestelling)
        {
            _context.Bestellingen.Add(bestelling);
            await _context.SaveChangesAsync();

            return bestelling;
        }

        public async Task<List<Bestelling>> GetAllAsync()
        {
            return await _context.Bestellingen.ToListAsync();
        }

        public async Task<Bestelling?> GetByIdAsync(int id)
        {
            return await _context.Bestellingen.FindAsync(id);
        }

        // U - Update: Pas de gegevens aan
        public async Task UpdateAsync(int id, Bestelling updatedBestelling)
        {
            Bestelling? existingBestelling = await _context.Bestellingen.FindAsync(id);

            if (existingBestelling != null)
            {
                existingBestelling = updatedBestelling;

                await _context.SaveChangesAsync();
            }
        }

        // D - Delete: Verwijder een laptop
        public async Task DeleteAsync(int id)
        {
            Bestelling? bestellingToDelete = await _context.Bestellingen.FindAsync(id);

            if (bestellingToDelete != null)
            {
                _context.Bestellingen.Remove(bestellingToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
