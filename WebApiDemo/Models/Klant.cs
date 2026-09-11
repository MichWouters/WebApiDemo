using WebApiDemo.Models;

namespace WebApiDemo.Models;

public class Klant : IModel
{
    public int Id { get; set; }

    public string Naam { get; set; } = default!;

    public string Voornaam { get; set; } = default!;

    public DateTime AangemaaktDatum { get; set; }

    public List<Bestelling>? Bestellingen { get; set; } = default!;
}