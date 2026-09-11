using Mapster;
using WebApiDemo.DTOs.Producten;

namespace WebApiDemo.Controllers
{
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

            // Map Model naar DTO
            ProductDto productDto = product.Adapt<ProductDto>();

            return Ok(productDto);
        }

        [HttpGet()]
        public async Task<ActionResult<ProductDto[]>> GetAllProducts(int id)
        {
            _logger.LogInformation($"GET request voor alle Producten");

            IEnumerable<Product> products = await _uow.ProductRepository.GetAllAsync();

            if (products == null || !products.Any())
            {
                _logger.LogWarning("Geen producten gevonden.");
                return NotFound("Geen producten gevonden.");
            }

            // Map colllectie van Modellen naar collectie van DTO's
            ProductDto[] productDtos = products.Adapt<ProductDto[]>();

            return Ok(productDtos);
        }
    }
}