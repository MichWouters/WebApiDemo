namespace WebApiDemo.DTOs.Bestellingen;

public class BesteldProductDto
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public int Aantal { get; set; }
    public decimal Prijs { get; set; }
}