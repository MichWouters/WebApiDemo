namespace WebApiDemo.Services
{
    public interface IBestellingService
    {
        BestellingCreatedDto Calculate(List<BesteldProductDto> producten, int accountLeeftijd);
    }
}