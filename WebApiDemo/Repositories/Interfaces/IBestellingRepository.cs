namespace WebApiDemo.Repositories;

public interface IBestellingRepository : IGenericRepository<Bestelling>
{
    Task<Bestelling?> GetBestellingMetKlantAsync(int id);

    Task<Bestelling?> GetBestellingMetDetailsAsync(int id);
}