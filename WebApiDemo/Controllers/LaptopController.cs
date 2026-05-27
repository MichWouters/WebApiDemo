using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Repositories;
using WebAPIDemo.Models;
// Zorg ervoor dat je Microsoft.Extensions.Logging hebt via using

namespace WebAPIDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LaptopsController : ControllerBase
{
    // Onze "ingrediënten" (Dependencies)
    private readonly ILaptopRepository _laptopRepository;
    private readonly ILogger<LaptopsController> _logger;

    // Vraag om en injecteer Dependencies
    public LaptopsController(ILaptopRepository laptopRepository, ILogger<LaptopsController> logger)
    {
        _laptopRepository = laptopRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Laptop>>> GetAlleLaptopsAsync()
    {
        _logger.LogInformation("GET request ontvangen voor alle laptops.");
        return Ok(await _laptopRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Laptop>> GetLaptopByIdAsync(int id)
    {
        _logger.LogInformation($"GET request voor laptop met ID: {id}");

        Laptop? laptop = await _laptopRepository.GetByIdAsync(id);

        if (laptop == null)
        {
            _logger.LogWarning($"Laptop met ID {id} werd niet gevonden.");
            return NotFound($"Helaas, we konden geen laptop vinden met Id {id}.");
        }

        return Ok(laptop);
    }

    [HttpPost]
    public async Task<ActionResult<Laptop>> CreateLaptopAsync(Laptop nieuweLaptop)
    {
        _logger.LogInformation($"Aanmaken van nieuwe laptop: {nieuweLaptop.Merk}");

        Laptop aangemaakteLaptop = await _laptopRepository.CreateAsync(nieuweLaptop);

        _logger.LogInformation($"Laptop succesvol aangemaakt met ID: {aangemaakteLaptop.Id}");
        return CreatedAtAction(nameof(GetLaptopByIdAsync), new { id = aangemaakteLaptop.Id }, aangemaakteLaptop);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLaptopAsync(int id, Laptop bijgewerkteLaptop)
    {
        _logger.LogInformation($"Update request voor laptop met ID: {id}");

        Laptop? bestaandeLaptop = await _laptopRepository.GetByIdAsync(id);

        if (bestaandeLaptop == null)
        {
            _logger.LogWarning($"Update mislukt: Laptop met ID {id} bestaat niet.");
            return NotFound($"Kan geen laptop bijwerken met Id {id}, omdat deze niet bestaat.");
        }

        await _laptopRepository.UpdateAsync(id, bijgewerkteLaptop);

        _logger.LogInformation($"Laptop met ID {id} is succesvol bijgewerkt.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLaptopAsync(int id)
    {
        _logger.LogInformation($"Delete request voor laptop met ID: {id}");

        Laptop? laptop = await _laptopRepository.GetByIdAsync(id);

        if (laptop == null)
        {
            _logger.LogError($"Delete mislukt: Laptop met ID {id} werd niet gevonden.");
            return NotFound($"Kan laptop met Id {id} niet verwijderen, omdat deze niet is gevonden.");
        }

        await _laptopRepository.DeleteAsync(id);

        _logger.LogInformation($"Laptop met ID {id} is succesvol verwijderd.");
        return NoContent();
    }
}