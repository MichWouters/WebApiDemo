namespace WebApiDemo.Repositories;

public class KlantRepository : GenericRepository<Klant>, IKlantRepository
{
    public KlantRepository(WebAPIDemoContext dbContext) : base(dbContext)
    {
    }

    public async Task<Klant?> GetKlantMetBestellingen(int klantId)
    {
        return await _context.Klanten
            .Include(k => k.Bestellingen)!
            .ThenInclude(b => b.OrderLijnen)
            .ThenInclude(ol => ol.Product)
            .FirstOrDefaultAsync(x => x.Id == klantId);
    }

    public async Task<bool> BestaatKlantAsync(string voornaam, string achternaam)
    {
        return await _context.Set<Klant>()
            .AnyAsync(x => x.Voornaam.ToLower() == voornaam.ToLower()
                           && x.Naam.ToLower() == achternaam.ToLower());
    }
}