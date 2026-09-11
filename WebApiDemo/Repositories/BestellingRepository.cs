namespace WebApiDemo.Repositories;

public class BestellingRepository : GenericRepository<Bestelling>, IBestellingRepository
{
    public BestellingRepository(WebAPIDemoContext context) : base(context)
    {
    }

    public async Task<Bestelling?> GetBestellingMetKlantAsync(int id)
    {
        // 1. Haal de gekoppelde klant op (Bestelling -> Klant)
        Bestelling? bestelling = await _context.Set<Bestelling>()
            .Include(b => b.Klant)
            .FirstOrDefaultAsync(b => b.Id == id);

        return bestelling;
    }

    public async Task<Bestelling?> GetBestellingMetDetailsAsync(int id)
    {
        return await _context.Set<Bestelling>()
            // 1. Haal de gekoppelde klant op (Bestelling → Klant)
            .Include(b => b.Klant)

            // 2. Haal de orderlijnen van deze bestelling op (Bestelling → OrderLijnen)
            .Include(b => b.OrderLijnen)
            // 3. Ga een niveau dieper: haal per orderlijn het product op (OrderLijn → Product)
            .ThenInclude(ol => ol.Product)

            // Voer de query uit
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}