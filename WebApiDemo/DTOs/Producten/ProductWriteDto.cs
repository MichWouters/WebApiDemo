using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.DTOs.Producten;

public class ProductWriteDto
{
    [Required(ErrorMessage = "De productnaam is verplicht.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "De naam moet tussen 2 en 100 tekens lang zijn.")]
    public string Naam { get; set; }

    [StringLength(500, ErrorMessage = "De beschrijving mag maximaal 500 tekens bevatten.")]
    public string? Beschrijving { get; set; } // Geen [Required] want dit veld mag null zijn (aangeduid door de ?)

    [Required(ErrorMessage = "De prijs is verplicht.")]
    [Range(0.01, 10000.00, ErrorMessage = "De prijs moet tussen € 0,01 en € 10.000 liggen.")]
    public decimal Prijs { get; set; }
}