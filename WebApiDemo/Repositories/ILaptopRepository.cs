using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories;

public interface ILaptopRepository
{
    List<Laptop> GetAll();
    Laptop? GetById(int id);
    Laptop Create(Laptop laptop);
    void Update(int id, Laptop laptop);
    void Delete(int id);
}