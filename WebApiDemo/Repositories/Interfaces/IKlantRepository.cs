namespace WebApiDemo.Repositories;

public interface IKlantRepository: IGenericRepository<Klant>
{
    Task<Klant?> GetKlantMetBestellingen(int klantId);
}