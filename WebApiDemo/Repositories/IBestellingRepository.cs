using WebAPIDemo.Models;

namespace WebApiDemo.Repositories
{
    public interface IBestellingRepository
    {
        Task<Bestelling> CreateAsync(Bestelling bestelling);
        Task DeleteAsync(int id);
        Task<List<Bestelling>> GetAllAsync();
        Task<Bestelling?> GetByIdAsync(int id);
        Task UpdateAsync(int id, Bestelling updatedBestelling);
    }
}