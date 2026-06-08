using WebApiDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebAPIDemoContext _context;

        // Private fields voor de repositories (voor lazy loading)
        private ILaptopRepository? _laptopRepository;
        private IGenericRepository<Product>? _productRepository;
        private IGenericRepository<Klant>? _klantRepository;
        private IGenericRepository<Bestelling>? _bestellingRepository;
        private IGenericRepository<OrderLijn>? _orderLijnRepository;

        public UnitOfWork(WebAPIDemoContext context)
        {
            _context = context;
        }

        // Lazy Loading property: Als _laptopRepository null is, maak hem dan aan.
        // We gebruiken de null-coalescing assignment operator (??=).
        public ILaptopRepository LaptopRepository =>
            _laptopRepository ??= new LaptopRepository(_context);

        // Voor de generieke repositories maken we direct een GenericRepository<T> aan
        public IGenericRepository<Product> ProductRepository =>
            _productRepository ??= new GenericRepository<Product>(_context);

        public IGenericRepository<Klant> KlantRepository =>
            _klantRepository ??= new GenericRepository<Klant>(_context);

        public IGenericRepository<Bestelling> BestellingRepository =>
            _bestellingRepository ??= new GenericRepository<Bestelling>(_context);

        public IGenericRepository<OrderLijn> OrderLijnRepository =>
            _orderLijnRepository ??= new GenericRepository<OrderLijn>(_context);

        // De centrale plek waar SaveChangesAsync wordt aangeroepen
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}