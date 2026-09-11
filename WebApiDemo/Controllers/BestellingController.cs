using Mapster;
using WebApiDemo.DTOs.Klanten;

namespace WebApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BestellingController : ControllerBase
{
    // 1. We gebruiken enkel nog IUnitOfWork, geen losse repositories meer!
    private readonly IUnitOfWork _uow;

    public BestellingController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    // GET: api/Bestelling/5/details
    // Haalt een bestelling op inclusief Klant, OrderLijnen en Producten
    [HttpGet("{id}/details")]
    public async Task<ActionResult<KlantMetBestellingenDto>> GetBestellingMetDetails(int id)
    {
        // Gebruik de specifieke repository-methode via de UoW
        Bestelling? bestelling = await _uow.BestellingRepository.GetBestellingMetDetailsAsync(id);

        if (bestelling == null)
        {
            return NotFound();
        }

        // Zet het model om naar een DTO
        KlantMetBestellingenDto dto = bestelling.Adapt<KlantMetBestellingenDto>();

        // Retourneer de DTO
        return Ok(dto);
    }

    // GET: api/Bestelling/klant/3
    // Haalt een klant op inclusief al zijn bestellingen en gekoppelde producten
    [HttpGet("klant/{klantId}")]
    public async Task<ActionResult<Klant>> GetBestellingenVanKlant(int klantId)
    {
        // Hier spreken we de KlantRepository aan via dezelfde UoW
        Klant? klant = await _uow.KlantRepository.GetKlantMetBestellingen(klantId);

        if (klant == null)
        {
            return NotFound();
        }

        // Zet het model om naar een DTO
        KlantDto dto = klant.Adapt<KlantDto>();

        return Ok(dto);
    }

    // POST: api/bestelling
    [HttpPost]
    public async Task<ActionResult<Bestelling>> PostBestelling(BestellingWriteDto dto)
    {
        // Map DTO naar Model
        Bestelling bestelling = dto.Adapt<Bestelling>();

        _uow.BestellingRepository.Add(bestelling);
        await _uow.SaveChangesAsync();

        // Teruggegeven model verplaatsen we tijdelijk naar NULL.
        // Later zullen we hier een bevestiging van de bestelling returnen
        return CreatedAtAction(nameof(PostBestelling), new { id = bestelling.Id }, null);
    }

    // PUT: api/bestelling/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBestelling(int id, BestellingWriteDto dto)
    {
        // Haal het bestaande object op op basis van het ID uit de URL
        var bestaandeBestelling = await _uow.BestellingRepository.GetByIdAsync(id);

        if (bestaandeBestelling == null)
        {
            return NotFound($"Bestelling met ID {id} werd niet gevonden.");
        }

        // Map de nieuwe waarden OVER het bestaande entiteit-object
        dto.Adapt(bestaandeBestelling);

        await _uow.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/bestelling/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBestelling(int id)
    {
        var bestelling = await _uow.BestellingRepository.GetByIdAsync(id);
        if (bestelling == null)
        {
            return NotFound();
        }

        _uow.BestellingRepository.Delete(bestelling);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}