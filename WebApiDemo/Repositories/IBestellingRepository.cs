using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories
{
    public interface IBestellingRepository : IGenericRepository<Bestelling>
    {
        Task<Bestelling?> GetBestellingMetKlantAsync(int id);

        Task<Bestelling?> GetBestellingMetDetailsAsync(int id);
    }
}