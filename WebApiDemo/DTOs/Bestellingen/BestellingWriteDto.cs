using System.ComponentModel.DataAnnotations;
using WebApiDemo.DTOs.OrderLijnen;

namespace WebApiDemo.DTOs.Bestellingen;

public class BestellingWriteDto
{
    [Required(ErrorMessage = "KlantId is verplicht.")]
    [Range(1, int.MaxValue, ErrorMessage = "Ongeldig KlantId.")]
    public int KlantId { get; set; }

    [Required(ErrorMessage = "Een bestelling moet orderlijnen bevatten.")]
    [MinLength(1, ErrorMessage = "Een bestelling moet minimaal 1 product (orderlijn) bevatten.")]
    public List<OrderLijnWriteDto> OrderLijnen { get; set; } = new();
}