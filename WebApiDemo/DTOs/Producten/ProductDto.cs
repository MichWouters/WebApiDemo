namespace WebApiDemo.DTOs.Producten;

public class ProductDto
{
    public int Id { get; set; }

    public string Naam { get; set; } = default!;

    public string? Beschrijving { get; set; }

    public decimal Prijs { get; set; }
}