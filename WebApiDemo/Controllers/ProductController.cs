using Mapster;


namespace WebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IUnitOfWork uow, ILogger<ProductController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        _logger.LogInformation($"GET request voor Product met ID: {id}");

        Product? product = await _uow.ProductRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning($"Product met ID {id} werd niet gevonden.");
            return NotFound($"Helaas, we konden geen product vinden met Id {id}.");
        }

        ProductDto productDto = product.Adapt<ProductDto>();

        return Ok(productDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        _logger.LogInformation("GET request voor alle Producten");

        IEnumerable<Product> products = await _uow.ProductRepository.GetAllAsync();

        if (products == null || !products.Any())
        {
            _logger.LogWarning("Geen producten gevonden.");
            return NotFound("Geen producten gevonden.");
        }

        ProductDto[] productDtos = products.Adapt<ProductDto[]>();

        return Ok(productDtos);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductWriteDto dto)
    {
        _logger.LogInformation("POST request voor een nieuw Product");

        // 1. Map DTO naar Domain Model
        Product product = dto.Adapt<Product>();

        // 2. Toevoegen via repository en opslaan
        _uow.ProductRepository.Add(product);
        await _uow.SaveChangesAsync();

        // 3. Map aangemaakt model terug naar Read DTO
        ProductDto createdProductDto = product.Adapt<ProductDto>();

        // 4. Stuur 201 Created terug met een 'Location' header naar GetProduct
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, createdProductDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductWriteDto dto)
    {
        _logger.LogInformation($"PUT request voor Product met ID: {id}");

        // 1. Haal het bestaande product op uit de database
        Product? bestaandProduct = await _uow.ProductRepository.GetByIdAsync(id);

        if (bestaandProduct == null)
        {
            _logger.LogWarning($"Product met ID {id} werd niet gevonden voor update.");
            return NotFound($"Kan product met ID {id} niet bijwerken omdat het niet bestaat.");
        }

        // 2. Map de nieuwe waarden OVER de bestaande entiteit (behoudt ID en niet-gewijzigde velden)
        dto.Adapt(bestaandProduct);

        // 3. Opslaan via Unit of Work
        _uow.ProductRepository.Update(bestaandProduct);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        _logger.LogInformation($"DELETE request voor Product met ID: {id}");

        Product? product = await _uow.ProductRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning($"Product met ID {id} werd niet gevonden voor verwijdering.");
            return NotFound($"Kan product met ID {id} niet verwijderen omdat het niet bestaat.");
        }

        _uow.ProductRepository.Delete(product);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}