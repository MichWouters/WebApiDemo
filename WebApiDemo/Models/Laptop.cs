namespace WebApiDemo.Models;

public class Laptop : IModel
{
    // De 'id' is essentieel om dit specifieke object later terug te vinden
    public int Id { get; set; }

    public string Merk { get; set; } = "";

    public string Processor { get; set; } = "";

    public int RamInGB { get; set; }

    public double Prijs { get; set; }

    public string GPU { get; set; } = "";
}