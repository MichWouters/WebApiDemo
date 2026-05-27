using WebAPIDemo.Models;

namespace WebApiDemo.Repositories
{
    public interface ILaptopRepository
    {
        Task<Laptop> CreateAsync(Laptop laptop);
        Task DeleteAsync(int id);
        Task<List<Laptop>> GetAllAsync();
        Task<Laptop?> GetByIdAsync(int id);
        Task UpdateAsync(int id, Laptop updatedLaptop);
    }
}