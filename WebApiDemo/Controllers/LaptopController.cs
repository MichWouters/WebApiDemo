using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;
using WebAPIDemo.Repositories;
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
    public ActionResult<List<Laptop>> GetAlleLaptops()
    {
        _logger.LogInformation("GET request ontvangen voor alle laptops.");
        return Ok(_laptopRepository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Laptop> GetLaptopById(int id)
    {
        _logger.LogInformation($"GET request voor laptop met ID: {id}");

        Laptop? laptop = _laptopRepository.GetById(id);

        if (laptop == null)
        {
            _logger.LogWarning($"Laptop met ID {id} werd niet gevonden.");
            return NotFound($"Helaas, we konden geen laptop vinden met Id {id}.");
        }

        return Ok(laptop);
    }

    [HttpGet("merk/{merk}")]
    public ActionResult<List<Laptop>> GetLaptopsByMerk(string merk)
    {
        _logger.LogInformation($"GET request ontvangen voor laptops met merk: {merk}");
        List<Laptop>? laptops = _laptopRepository.GetLaptopsByMerk(merk);

        if (laptops == null || laptops.Count == 0)
        {
            _logger.LogWarning($"Geen laptops gevonden met merk: {merk}");
            return NotFound($"Helaas, we konden geen laptops vinden met merk {merk}.");
        }

        return Ok(laptops);
    }

    [HttpPost]
    public ActionResult<Laptop> CreateLaptop(Laptop nieuweLaptop)
    {
        _logger.LogInformation($"Aanmaken van nieuwe laptop: {nieuweLaptop.Merk}");

        Laptop aangemaakteLaptop = _laptopRepository.Create(nieuweLaptop);

        _logger.LogInformation($"Laptop succesvol aangemaakt met ID: {aangemaakteLaptop.Id}");
        return CreatedAtAction(nameof(GetLaptopById), new { id = aangemaakteLaptop.Id }, aangemaakteLaptop);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateLaptop(int id, Laptop bijgewerkteLaptop)
    {
        _logger.LogInformation($"Update request voor laptop met ID: {id}");

        Laptop? bestaandeLaptop = _laptopRepository.GetById(id);

        if (bestaandeLaptop == null)
        {
            _logger.LogWarning($"Update mislukt: Laptop met ID {id} bestaat niet.");
            return NotFound($"Kan geen laptop bijwerken met Id {id}, omdat deze niet bestaat.");
        }

        _laptopRepository.Update(id, bijgewerkteLaptop);

        _logger.LogInformation($"Laptop met ID {id} is succesvol bijgewerkt.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteLaptop(int id)
    {
        _logger.LogInformation($"Delete request voor laptop met ID: {id}");

        Laptop? laptop = _laptopRepository.GetById(id);

        if (laptop == null)
        {
            _logger.LogError($"Delete mislukt: Laptop met ID {id} werd niet gevonden.");
            return NotFound($"Kan laptop met Id {id} niet verwijderen, omdat deze niet is gevonden.");
        }

        _laptopRepository.Delete(id);

        _logger.LogInformation($"Laptop met ID {id} is succesvol verwijderd.");
        return NoContent();
    }
}