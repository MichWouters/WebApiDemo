using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;
using WebAPIDemo.Repositories;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellingController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        // Dependency Injection
        public BestellingController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/bestellingen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bestelling>>> GetBestellingen()
        {
            var bestellingen = await _uow.BestellingRepository.GetAllAsync();
            return Ok(bestellingen);
        }

        // GET: api/bestellingen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bestelling>> GetBestelling(int id)
        {
            var bestelling = await _uow.BestellingRepository.GetByIdAsync(id);

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
            _uow.BestellingRepository.Add(bestelling);
            await _uow.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBestelling), new { id = bestelling.Id }, bestelling);
        }

        // PUT: api/bestellingen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestelling(int id, Bestelling bestelling)
        {
            if (id != bestelling.Id)
            {
                return BadRequest("De ID in de URL komt niet overeen met het ID in de data.");
            }

            _uow.BestellingRepository.Update(bestelling);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/bestellingen/5
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
}