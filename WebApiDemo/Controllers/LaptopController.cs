using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers;

[ApiController]
[Route("api/[controller]")] // Route wordt: api/laptops
public class LaptopsController : ControllerBase
{
    // Onze tijdelijke "database" in het geheugen
    private static List<Laptop> laptops = new List<Laptop>
    {
        new Laptop { Id = 1, Merk = "Dell", Processor = "Intel i7", RamInGB = 16, Prijs = 1200.00, GPU = "Iris Xe" },
        new Laptop { Id = 2, Merk = "Apple", Processor = "M3 Pro", RamInGB = 18, Prijs = 2500.00, GPU = "Apple GPU" },
        new Laptop { Id = 3, Merk = "Lenovo", Processor = "AMD Ryzen 7", RamInGB = 32, Prijs = 1400.00, GPU = "RTX 4060" }
    };

    // 1. GET: Alle laptops ophalen
    // Route: GET api/laptops
    [HttpGet]
    public ActionResult<List<Laptop>> GetAlleLaptops()
    {
        // We sturen een 200 OK status terug samen met de lijst
        return Ok(laptops);
    }

    // 2. GET: Eén specifieke laptop op basis van Id
    // Route: GET api/laptops/1
    [HttpGet("{id}")]
    public ActionResult<Laptop> GetLaptopById(int id)
    {
        // Zoek de laptop in de lijst die de gevraagde Id heeft
        var laptop = laptops.FirstOrDefault(x => x.Id == id);

        // Als de laptop niet bestaat, stuur dan een 404 terug
        if (laptop == null)
        {
            return NotFound($"Helaas, we konden geen laptop vinden met Id {id}.");
        }

        return Ok(laptop);
    }

    // 3. POST: Een nieuwe laptop toevoegen
    // Route: POST api/laptops
    [HttpPost]
    public ActionResult<Laptop> CreateLaptop(Laptop nieuweLaptop)
    {
        // Omdat we geen database hebben die automatisch ID's genereert,
        // zoeken we zelf de hoogste ID en tellen we er 1 bij op.
        int nieuweId = laptops.Max(x => x.Id) + 1;
        nieuweLaptop.Id = nieuweId;

        // Voeg de nieuwe laptop toe aan onze in-memory lijst
        laptops.Add(nieuweLaptop);

        // We retourneren een 201 Created statuscode.
        // Dit vertelt de client dat het object succesvol is aangemaakt én waar het te vinden is.
        return CreatedAtAction(nameof(GetLaptopById), new { id = nieuweLaptop.Id }, nieuweLaptop);
    }

    // 4. PUT: Een bestaande laptop volledig bijwerken
    // Route: PUT api/laptops/1
    [HttpPut("{id}")]
    public ActionResult UpdateLaptop(int id, Laptop bijgewerkteLaptop)
    {
        // 1. Zoek de laptop die momenteel in de lijst staat
        var bestaandeLaptop = laptops.FirstOrDefault(x => x.Id == id);

        // 2. Als de laptop niet bestaat, sturen we een 404 Not Found
        if (bestaandeLaptop == null)
        {
            return NotFound($"Kan geen laptop bijwerken met Id {id}, omdat deze niet bestaat.");
        }

        // 3. Overschrijf de eigenschappen met de nieuwe data uit de body
        bestaandeLaptop.Merk = bijgewerkteLaptop.Merk;
        bestaandeLaptop.Processor = bijgewerkteLaptop.Processor;
        bestaandeLaptop.RamInGB = bijgewerkteLaptop.RamInGB;
        bestaandeLaptop.Prijs = bijgewerkteLaptop.Prijs;
        bestaandeLaptop.GPU = bijgewerkteLaptop.GPU;

        // 4. Update succesvol! We sturen een 204 No Content terug.
        return NoContent();
    }

    // 5. DELETE: Een laptop verwijderen
    // Route: DELETE api/laptops/1
    [HttpDelete("{id}")]
    public ActionResult DeleteLaptop(int id)
    {
        // 1. Zoek de laptop in onze lijst
        var laptop = laptops.FirstOrDefault(x => x.Id == id);

        // 2. Bestaat hij niet? Dan kunnen we hem ook niet verwijderen -> 404!
        if (laptop == null)
        {
            return NotFound($"Kan laptop met Id {id} niet verwijderen, omdat deze niet is gevonden.");
        }

        // 3. Verwijder het object uit de lijst
        laptops.Remove(laptop);

        // 4. Succesvol verwijderd -> We sturen een 204 No Content terug
        return NoContent();
    }
}