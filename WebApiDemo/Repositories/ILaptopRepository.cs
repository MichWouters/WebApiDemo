namespace WebAPIDemo.Repositories
{
    // We erven alle standaard methoden over voor het type Laptop
    public interface ILaptopRepository : IGenericRepository<Laptop>
    {
        // We voegen enkel de specifieke methoden toe
        Task<IEnumerable<Laptop>> GetLaptopsByMerkAsync(string merk);
    }
}