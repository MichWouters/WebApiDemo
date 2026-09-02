namespace WebAPIDemo.Repositories;

public interface IKlantRepository: IGenericRepository<Klant>
{
    Task<Klant?> GetKlantMetBestellingen(int klantId);
}