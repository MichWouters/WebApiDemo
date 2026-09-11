using Mapster;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KlantController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<KlantController> _logger;

        public KlantController(IUnitOfWork uow, ILogger<KlantController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KlantDto>> GetKlant(int id)
        {
            _logger.LogInformation($"GET request voor Klant met ID: {id}");

            Klant? klant = await _uow.KlantRepository.GetByIdAsync(id);

            if (klant == null)
            {
                _logger.LogWarning($"Klant met ID {id} werd niet gevonden.");
                return NotFound($"Geen klant gevonden met ID {id}.");
            }

            KlantDto klantDto = klant.Adapt<KlantDto>();

            return Ok(klantDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlantDto>>> GetAllKlanten()
        {
            _logger.LogInformation("GET request voor alle Klanten");

            IEnumerable<Klant> klanten = await _uow.KlantRepository.GetAllAsync();

            if (klanten == null || !klanten.Any())
            {
                return NotFound("Geen klanten gevonden.");
            }

            KlantDto[] klantDtos = klanten.Adapt<KlantDto[]>();

            return Ok(klantDtos);
        }

        [HttpPost]
        public async Task<ActionResult<KlantDto>> PostKlant(KlantWriteDto dto)
        {
            _logger.LogInformation("POST request voor een nieuwe Klant");

            // 1. Handmatige/business validatie
            await ValideerKlantAsync(dto);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // 2. Map DTO naar Domain Model
            Klant klant = dto.Adapt<Klant>();

            // Interne waarden instellen die de client niet mag meegeven
            klant.AangemaaktDatum = DateTime.UtcNow;

            // 3. Opslaan via repository
            _uow.KlantRepository.Add(klant);
            await _uow.SaveChangesAsync();

            // 4. Map aangemaakt model terug naar Read DTO (KlantDto)
            KlantDto createdKlantDto = klant.Adapt<KlantDto>();

            return CreatedAtAction(nameof(GetKlant), new { id = klant.Id }, createdKlantDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKlant(int id, KlantWriteDto dto)
        {
            _logger.LogInformation($"PUT request voor Klant met ID: {id}");

            Klant? bestaandeKlant = await _uow.KlantRepository.GetByIdAsync(id);

            if (bestaandeKlant == null)
            {
                _logger.LogWarning($"Klant met ID {id} werd niet gevonden voor update.");
                return NotFound($"Kan klant met ID {id} niet bijwerken omdat deze niet bestaat.");
            }

            await ValideerKlantAsync(dto);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // Map de nieuwe waarden OVER de bestaande entiteit
            dto.Adapt(bestaandeKlant);

            _uow.KlantRepository.Update(bestaandeKlant);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlant(int id)
        {
            _logger.LogInformation($"DELETE request voor Klant met ID: {id}");

            Klant? klant = await _uow.KlantRepository.GetByIdAsync(id);

            if (klant == null)
            {
                return NotFound($"Kan klant met ID {id} niet verwijderen omdat deze niet bestaat.");
            }

            _uow.KlantRepository.Delete(klant);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private async Task ValideerKlantAsync(KlantWriteDto dto)
        {
            // Voorbeeld van een custom business rule: controleer op dubbele klantnamen
            bool klantBestaat = await _uow.KlantRepository.BestaatKlantAsync(dto.Naam, dto.Voornaam);

            if (klantBestaat)
            {
                ModelState.AddModelError(nameof(dto.Naam), $"Er bestaat al een klant met de naam '{dto.Voornaam} {dto.Naam}'.");
            }
        }
    }
}