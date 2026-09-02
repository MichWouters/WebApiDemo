namespace WebAPIDemo.Repositories;

public class KlantRepository : GenericRepository<Klant>, IKlantRepository
{
    public KlantRepository(WebAPIDemoContext dbContext) : base(dbContext)
    {
    }

    public async Task<Klant?> GetKlantMetBestellingen(int klantId)
    {
        return await _context.Set<Klant>()
            .Include(k => k.Bestellingen)!
            .ThenInclude(b => b.OrderLijnen)
            .ThenInclude(ol => ol.Product)
            .FirstOrDefaultAsync(x => x.Id == klantId);
    }
}