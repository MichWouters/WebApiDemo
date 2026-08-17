using WebApiDemo.Repositories;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories
{
    public interface IUnitOfWork
    {
        // Specifieke repositories omdat deze extra methoden hebben
        ILaptopRepository LaptopRepository { get; }
        IBestellingRepository BestellingRepository { get; }

        // Generieke repositories voor entiteiten die enkel basis-CRUD nodig hebben
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Klant> KlantRepository { get; }
        IGenericRepository<OrderLijn> OrderLijnRepository { get; }

        // De centrale Save methode (asynchroon)
        Task<int> SaveChangesAsync();
    }
}