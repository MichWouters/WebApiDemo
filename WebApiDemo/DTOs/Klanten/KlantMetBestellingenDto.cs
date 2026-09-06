using WebApiDemo.DTOs.Bestellingen;

namespace WebApiDemo.DTOs.Klanten;

public class KlantMetBestellingenDto
{
    public int Id { get; set; }
    public string KlantNaam { get; set; } = default!;
    public decimal TotaalPrijs { get; set; }
    public BesteldProductDto[] BesteldeProducten { get; set; } = [];
}