using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.DTOs.OrderLijnen;

public class OrderLijnWriteDto
{
    [Required(ErrorMessage = "Het aantal is verplicht.")]
    [Range(1, 100, ErrorMessage = "Het aantal moet groter zijn dan 0.")]
    public int Aantal { get; set; }

    [Required(ErrorMessage = "Een geldig ProductId is verplicht.")]
    [Range(1, int.MaxValue, ErrorMessage = "Ongeldig ProductId.")]
    public int ProductId { get; set; }
}