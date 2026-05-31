using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;
using WebApiDemo.Repositories;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellingController : ControllerBase
    {
        private readonly IBestellingRepository _bestellingRepo;

        // Dependency Injection
        public BestellingController(IBestellingRepository bestellingRepo)
        {
            _bestellingRepo = bestellingRepo;
        }

        // GET: api/bestellingen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bestelling>>> GetBestellingen()
        {
            var bestellingen = await _bestellingRepo.GetAllAsync();
            return Ok(bestellingen);
        }

        // GET: api/bestellingen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bestelling>> GetBestelling(int id)
        {
            var bestelling = await _bestellingRepo.GetByIdAsync(id);

            if (bestelling == null)
            {
                return NotFound($"Bestelling met ID {id} is niet gevonden.");
            }

            return Ok(bestelling);
        }

        // POST: api/bestellingen
        [HttpPost]
        public async Task<ActionResult<Bestelling>> PostBestelling(Bestelling bestelling)
        {
            var nieuweBestelling = await _bestellingRepo.CreateAsync(bestelling);

            return CreatedAtAction(nameof(GetBestelling), new { id = nieuweBestelling.Id }, nieuweBestelling);
        }

        // PUT: api/bestellingen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestelling(int id, Bestelling bestelling)
        {
            if (id != bestelling.Id)
            {
                return BadRequest("De ID in de URL komt niet overeen met het ID in de data.");
            }

            await _bestellingRepo.UpdateAsync(id, bestelling);
            return NoContent();
        }

        // DELETE: api/bestellingen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBestelling(int id)
        {
            var bestelling = await _bestellingRepo.GetByIdAsync(id);
            if (bestelling == null)
            {
                return NotFound();
            }

            await _bestellingRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}