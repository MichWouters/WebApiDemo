namespace WebApiDemo.DTOs.Klanten;

public class KlantDto
{
    public int Id { get; set; }

    public string Naam { get; set; }

    public string Voornaam { get; set; }

    public DateTime AangemaaktDatum { get; set; }

    public BesteldProductDto[] BesteldeProducten{ get; set; }
}