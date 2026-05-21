using WebAPIDemo.Models;

namespace WebAPIDemo.Repositories;

public class InMemoryRepository : ILaptopRepository
{
    // Onze tijdelijke "database" in het geheugen
    private List<Laptop> laptops = new List<Laptop>
    {
        new Laptop { Id = 1, Merk = "Dell", Processor = "Intel i7", RamInGB = 16, Prijs = 1200.00, GPU = "Iris Xe" },
        new Laptop { Id = 2, Merk = "Apple", Processor = "M3 Pro", RamInGB = 18, Prijs = 2500.00, GPU = "Apple GPU" },
        new Laptop { Id = 3, Merk = "Lenovo", Processor = "AMD Ryzen 7", RamInGB = 32, Prijs = 1400.00, GPU = "RTX 4060" }
    };

    public List<Laptop> GetAll()
    {
        return laptops;
    }

    public Laptop? GetById(int id)
    {
        // Zoek de laptop in de lijst die de gevraagde Id heeft
        return laptops.FirstOrDefault(x => x.Id == id);
    }

    public Laptop Create(Laptop nieuweLaptop)
    {
        // Omdat we geen database hebben die automatisch ID's genereert, zoeken we zelf de hoogste ID en tellen we er 1 bij op.
        int nieuweId = laptops.Max(x => x.Id) + 1;
        nieuweLaptop.Id = nieuweId;

        // Voeg de nieuwe laptop toe aan onze in-memory lijst
        laptops.Add(nieuweLaptop);
        return nieuweLaptop;
    }

    public void Update(int id, Laptop bijgewerkteLaptop)
    {
        // Zoek de laptop die momenteel in de lijst staat
        var bestaandeLaptop = laptops.FirstOrDefault(x => x.Id == id);

        if (bestaandeLaptop != null)
        {
            // Overschrijf de eigenschappen met de nieuwe data uit de body
            bestaandeLaptop.Merk = bijgewerkteLaptop.Merk;
            bestaandeLaptop.Processor = bijgewerkteLaptop.Processor;
            bestaandeLaptop.RamInGB = bijgewerkteLaptop.RamInGB;
            bestaandeLaptop.Prijs = bijgewerkteLaptop.Prijs;
            bestaandeLaptop.GPU = bijgewerkteLaptop.GPU;
        }
    }

    public void Delete(int id)
    {
        // Zoek de laptop in onze lijst
        var laptop = laptops.FirstOrDefault(x => x.Id == id);

        if (laptop != null)
        {
            // Verwijder het object uit de lijst
            laptops.Remove(laptop);
        }
    }
}