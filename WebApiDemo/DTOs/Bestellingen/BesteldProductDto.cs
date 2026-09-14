namespace WebApiDemo.DTOs.Bestellingen;

public class BesteldProductDto
{
    public int Id { get; set; }
    public string ProductNaam { get; set; }
    public int Aantal { get; set; }
    public decimal ProductPrijs { get; set; }
}