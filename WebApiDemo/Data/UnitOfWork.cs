namespace WebApiDemo.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly WebAPIDemoContext _context;

    // Private fields voor de repositories (voor lazy loading)
    private ILaptopRepository? _laptopRepository;
    private IBestellingRepository? _bestellingRepository;
    private IKlantRepository _klantRepository;

    private IGenericRepository<Product>? _productRepository;
    private IGenericRepository<OrderLijn>? _orderLijnRepository;

    public UnitOfWork(WebAPIDemoContext context)
    {
        _context = context;
    }

    // Lazy Loading properties: Als een repo null is, maak hem dan aan.
    // We gebruiken de null-coalescing assignment operator (??=).
    // Aangezien LaptopRepository specifieke methodes bevat, kiezen we voor een specifiek type en geen GenericRepository
    public ILaptopRepository LaptopRepository =>
        _laptopRepository ??= new LaptopRepository(_context);

    public IBestellingRepository BestellingRepository =>
        _bestellingRepository ??= new BestellingRepository(_context);
    
    public IKlantRepository KlantRepository =>
        _klantRepository ??= new KlantRepository(_context);

    // Voor de generieke repositories maken we direct een GenericRepository<T> aan
    public IGenericRepository<Product> ProductRepository =>
        _productRepository ??= new GenericRepository<Product>(_context);

    public IGenericRepository<OrderLijn> OrderLijnRepository =>
        _orderLijnRepository ??= new GenericRepository<OrderLijn>(_context);

    // De centrale plek waar SaveChangesAsync wordt aangeroepen
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}