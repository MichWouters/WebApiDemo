using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;
using WebAPIDemo.Repositories;
// Zorg ervoor dat je Microsoft.Extensions.Logging hebt via using

namespace WebAPIDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    // Onze "ingrediënten" (Dependencies)
    private readonly IUnitOfWork _uow;
    private readonly ILogger<ProductController> _logger;

    // Vraag om en injecteer Dependencies
    public ProductController(IUnitOfWork uow, ILogger<ProductController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Laptop>>> GetAlleLaptopsAsync()
    {
        _logger.LogInformation("GET request ontvangen voor alle laptops.");
        return Ok(await _uow.LaptopRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Laptop>> GetLaptopByIdAsync(int id)
    {
        _logger.LogInformation($"GET request voor laptop met ID: {id}");

        Laptop? laptop = await _uow.LaptopRepository.GetByIdAsync(id);

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

        // 1. Voeg de actie toe aan de Change Tracker via de repository
        _uow.LaptopRepository.Add(nieuweLaptop);

        // 2. Schrijf de wijzigingen daadwerkelijk weg via de Unit of Work
        await _uow.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateLaptopAsync), new { id = nieuweLaptop.Id }, nieuweLaptop);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLaptopAsync(int id, Laptop bijgewerkteLaptop)
    {
        _logger.LogInformation($"Update request voor laptop met ID: {id}");

        if (id != bijgewerkteLaptop.Id)
        {
            return BadRequest();
        }

        Laptop? bestaandeLaptop = await _uow.LaptopRepository.GetByIdAsync(id);

        if (bestaandeLaptop == null)
        {
            _logger.LogWarning($"Update mislukt: Laptop met ID {id} bestaat niet.");
            return NotFound($"Kan geen laptop bijwerken met Id {id}, omdat deze niet bestaat.");
        }

        _uow.LaptopRepository.Update(bijgewerkteLaptop);
        await _uow.SaveChangesAsync();

        _logger.LogInformation($"Laptop met ID {id} is succesvol bijgewerkt.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLaptopAsync(int id)
    {
        _logger.LogInformation($"Delete request voor laptop met ID: {id}");

        Laptop? laptop = await _uow.LaptopRepository.GetByIdAsync(id);

        if (laptop == null)
        {
            _logger.LogError($"Delete mislukt: Laptop met ID {id} werd niet gevonden.");
            return NotFound($"Kan laptop met Id {id} niet verwijderen, omdat deze niet is gevonden.");
        }

        _uow.LaptopRepository.Delete(laptop);
        await _uow.SaveChangesAsync();

        _logger.LogInformation($"Laptop met ID {id} is succesvol verwijderd.");
        return NoContent();
    }
}