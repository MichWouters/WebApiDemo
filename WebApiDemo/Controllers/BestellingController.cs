using Mapster;
using WebApiDemo.Services;

namespace WebApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BestellingController : ControllerBase
{
    // 1. We gebruiken enkel nog IUnitOfWork, geen losse repositories meer!
    private readonly IUnitOfWork _uow;

    private readonly IBestellingService _service;

    public BestellingController(IUnitOfWork uow, IBestellingService service)
    {
        _uow = uow;
        _service = service;
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

    [HttpPost]
    public async Task<ActionResult<BestellingCreatedDto>> PostBestelling(BestellingWriteDto dto)
    {
        // Custom validatie uitvoeren (bijv. bestaat de klant, zijn de producten op voorraad?)
        await ValideerBestellingAsync(dto);

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        // Map DTO naar model
        Bestelling bestelling = dto.Adapt<Bestelling>();

        // Opslaan in database
        _uow.BestellingRepository.Add(bestelling);
        await _uow.SaveChangesAsync();

        var resultDto = await CalculateResultDto(bestelling.Id);

        // Retourneer 201 Created met de gevulde BestellingCreatedDto
        return CreatedAtAction(nameof(PostBestelling), new { id = bestelling.Id }, resultDto);
    }

    private async Task<BestellingCreatedDto> CalculateResultDto(int bestellingId)
    {
        Bestelling? models = await _uow.BestellingRepository.GetBestellingMetDetailsAsync(bestellingId);
        int accountLeeftijd = DateTime.UtcNow.Year - models.Klant.AangemaaktDatum.Year;
        List<BesteldProductDto>? productDtos = models.OrderLijnen.Adapt<List<BesteldProductDto>>();

        return _service.Calculate(productDtos, accountLeeftijd);
    }

    // PUT: api/bestelling/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBestelling(int id, BestellingWriteDto dto)
    {
        // Haal het bestaande object op op basis van het ID uit de URL
        Bestelling? bestaandeBestelling = await _uow.BestellingRepository.GetByIdAsync(id);

        if (bestaandeBestelling == null)
        {
            return NotFound($"Bestelling met ID {id} werd niet gevonden.");
        }

        await ValideerBestellingAsync(dto);

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
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
        Bestelling? bestelling = await _uow.BestellingRepository.GetByIdAsync(id);
        if (bestelling == null)
        {
            return NotFound();
        }

        _uow.BestellingRepository.Delete(bestelling);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    private async Task ValideerBestellingAsync(BestellingWriteDto dto)
    {
        // 1. Controleer of de klant bestaat
        bool klantBestaat = await _uow.KlantRepository.ExistsAsync(dto.KlantId);
        if (!klantBestaat)
        {
            ModelState.AddModelError(nameof(dto.KlantId), $"Klant met ID {dto.KlantId} bestaat niet.");
        }

        // 2. Haal alle bestaande Product IDs op en vergelijk
        int[] bestaandeProductIds = await _uow.ProductRepository.GetExistingIdsAsync();
        int[] besteldeProductenIds = dto.OrderLijnen.Select(ol => ol.ProductId).ToArray();

        foreach (int productId in besteldeProductenIds)
        {
            if (!bestaandeProductIds.Contains(productId))
            {
                ModelState.AddModelError(nameof(dto.OrderLijnen), $"Product met ID {productId} bestaat niet.");
            }
        }
    }
}