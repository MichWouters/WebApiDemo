namespace WebApiDemo.DTOs.Bestellingen;

public class BestellingCreatedDto
{
    public decimal TotaalPrijs { get; set; }
    public bool HeeftVipGoodiebag { get; set; }
    public List<BesteldProductDto> ProductenInBestelling { get; set; }
    public decimal Verzendkosten { get; set; }
}
