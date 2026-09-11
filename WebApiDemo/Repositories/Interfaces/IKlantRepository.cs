namespace WebApiDemo.Repositories;

public interface IKlantRepository: IGenericRepository<Klant>
{
    Task<Klant?> GetKlantMetBestellingen(int klantId);

    Task<bool> BestaatKlantAsync(string voornaam, string achternaam);
}