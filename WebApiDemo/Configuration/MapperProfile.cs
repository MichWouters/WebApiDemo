using Mapster;
using WebApiDemo.DTOs.Klanten;

namespace WebApiDemo.Configuration;

public class MapperProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Bestelling, KlantMetBestellingenDto>()
            // Namen samenvoegen
            .Map(dest => dest.KlantNaam, src => $"{src.Klant.Voornaam} {src.Klant.Naam}")

            // Totale prijs berekenen door alle orderlijnen te overlopen. (Bad Practice)
            .Map(dest => dest.TotaalPrijs, src => src.OrderLijnen.Sum(ol => (decimal)ol.Aantal * ol.Product!.Prijs))

            // Relatie mappen (Mapster snapt automatisch dat hij hiervoor de bovenstaande regel 1 moet gebruiken)
            .Map(dest => dest.BesteldeProducten, src => src.OrderLijnen);

        // De namen van deze Properties zijn hetzelfde, maar zitten een niveau dieper in de Source.
        // Daarom moeten we deze manueel mappen
        config.NewConfig<OrderLijn, BesteldProductDto>()
            .Map(dest => dest.Naam, src => src.Product!.Naam)
            .Map(dest => dest.Prijs, src => src.Product!.Prijs);

        //Geavanceerdere mappings

        // 1. Vertel Mapster hoe één OrderLijn naar één BesteldProductDto vertaald moet worden
        config.NewConfig<OrderLijn, BesteldProductDto>()
            .Map(dest => dest.Id, src => src.Product.Id)
            .Map(dest => dest.Aantal, src => src.Aantal)
            .Map(dest => dest.Naam, src => src.Product.Naam)
            .Map(dest => dest.Prijs, src => src.Product.Prijs);

        // 2. Vertel Mapster hoe de Klant naar de KlantDto vertaald moet worden
        config.NewConfig<Klant, KlantDto>()
            // Id, Naam, Voornaam en AangemaaktDatum worden automatisch gemapt!
            // Gebruik SelectMany om alle OrderLijnen uit alle Bestellingen samen te voegen tot één platte lijst
            .Map(dest => dest.BesteldeProducten, src => src.Bestellingen.SelectMany(b => b.OrderLijnen));
    }
}