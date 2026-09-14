namespace WebApiDemo.Services;

public class BestellingService : IBestellingService
{
    private const decimal KortingPercentage = 0.05m;
    private const decimal DrempelKortingBestelling = 500.00m;
    private const int MinimumAantalJarenVoorKorting = 5;
    private const decimal StandaardVerzendkosten = 25.00m;
    private const decimal MinimumTotaalPrijsVoorGratisVerzending = 100.00m;
    private const int MinimumAantalBesteldVoorVIPPackage = 10;

    public BestellingCreatedDto Calculate(List<BesteldProductDto> producten, int accountLeeftijd)
    {
        decimal brutoProductWaarde = producten.Sum(p => p.ProductPrijs * p.Aantal);
        decimal kortingsPercentage = BerekenKortingsPercentage(brutoProductWaarde, accountLeeftijd);
        decimal nettoProductWaarde = brutoProductWaarde * (1m - kortingsPercentage);
        decimal verzendkosten = BerekenVerzendkosten(nettoProductWaarde);

        decimal totaalAfgerond = RondAfNaarBovenMet2CijfersAchterDeKomma(nettoProductWaarde + verzendkosten);

        return new BestellingCreatedDto
        {
            ProductenInBestelling = producten,
            Verzendkosten = verzendkosten,
            TotaalPrijs = totaalAfgerond,
            HeeftVipGoodiebag = HeeftVipGoodiebag(producten)
        };
    }

    private decimal RondAfNaarBovenMet2CijfersAchterDeKomma(decimal totaalOnafgerond)
    {
        return Math.Ceiling(totaalOnafgerond * 100m) / 100m;
    }

    private decimal BerekenKortingsPercentage(decimal brutoWaarde, int accountLeeftijd)
    {
        decimal percentage = 0m;

        if (accountLeeftijd > MinimumAantalJarenVoorKorting)
        {
            percentage += KortingPercentage;
        }

        if (brutoWaarde > DrempelKortingBestelling)
        {
            percentage += KortingPercentage;
        }

        return percentage;
    }

    private decimal BerekenVerzendkosten(decimal nettoWaarde)
    {
        if (nettoWaarde > MinimumTotaalPrijsVoorGratisVerzending)
        {
            return 0.00m;
        }

        return StandaardVerzendkosten;
    }

    private bool HeeftVipGoodiebag(List<BesteldProductDto> producten)
    {
        return producten.Any(p => p.Aantal > MinimumAantalBesteldVoorVIPPackage);
    }
}