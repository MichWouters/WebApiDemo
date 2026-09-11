namespace WebApiDemo.Data;

public interface IUnitOfWork
{
    // Specifieke repositories omdat deze extra methoden hebben
    ILaptopRepository LaptopRepository { get; }
    IBestellingRepository BestellingRepository { get; }
    IKlantRepository KlantRepository { get; }

    // Generieke repositories voor entiteiten die enkel basis-CRUD nodig hebben
    IGenericRepository<OrderLijn> OrderLijnRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }

    // De centrale Save methode (asynchroon)
    Task<int> SaveChangesAsync();
}