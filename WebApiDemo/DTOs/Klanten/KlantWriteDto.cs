using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.DTOs.Klanten;

public class KlantWriteDto
{
    [Required(ErrorMessage = "De achternaam is verplicht.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "De achternaam moet tussen 2 en 50 tekens lang zijn.")]
    public string Naam { get; set; }

    [Required(ErrorMessage = "De voornaam is verplicht.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "De voornaam moet tussen 2 en 50 tekens lang zijn.")]
    public string Voornaam { get; set; }
}