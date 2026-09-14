using NUnit.Framework;
using WebApiDemo.DTOs.Bestellingen;
using WebApiDemo.Services;

namespace WebApiDemo.Tests;

[TestFixture]
public class BestellingTests
{
    private BestellingService classUnderTest;

    [SetUp]
    public void Setup()
    {
        classUnderTest = new BestellingService();
    }

    // =========================================================================
    // NIVEAU 1: Basis- en Sanity checks (Structuur & Mapping)
    // =========================================================================

    [Test]
    public void CalculateTotal_ReturnsValidBestellingCreatedDto()
    {
        // Sanity check: test of het DTO-object correct wordt opgebouwd
        var producten = new List<BesteldProductDto> { new() { ProductPrijs = 50.00m, Aantal = 1 } };

        var result = classUnderTest.Calculate(producten, 2);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<BestellingCreatedDto>());
        Assert.That(result.ProductenInBestelling, Is.Not.Null);
    }

    [Test]
    public void CalculateTotal_RetourneertExactDezelfdeProductenInResultaat()
    {
        var producten = new List<BesteldProductDto>
        {
            new BesteldProductDto { ProductNaam = "Product A", ProductPrijs = 10.00m, Aantal = 2 },
            new BesteldProductDto { ProductNaam = "Product B", ProductPrijs = 20.00m, Aantal = 1 }
        };

        var result = classUnderTest.Calculate(producten, accountLeeftijd: 1);

        Assert.That(result.ProductenInBestelling, Has.Count.EqualTo(2));
        Assert.That(result.ProductenInBestelling, Is.EquivalentTo(producten));
    }

    // =========================================================================
    // NIVEAU 2: Enkelvoudige Business Rules & Grenswaarden (Single Condition)
    // =========================================================================

    // Rule: VIP Goodiebag bij > 10 stuks
    [TestCase(10, false)] // Exact 10 = geen VIP tas
    [TestCase(11, true)]  // Meer dan 10 = wel een VIP tas
    public void CalculateTotal_MeerDan10ItemsVanZelfdeProduct_VoegtVipGoodiebagToe(int aantalItems, bool verwachtVip)
    {
        var producten = new List<BesteldProductDto>
        {
            new BesteldProductDto { ProductNaam = "Muisjes", ProductPrijs = 5.00m, Aantal = aantalItems }
        };

        BestellingCreatedDto result = classUnderTest.Calculate(producten, accountLeeftijd: 2);

        Assert.That(result.HeeftVipGoodiebag, Is.EqualTo(verwachtVip));
    }

    // Rule: Verzendkosten (25 euro vs gratis boven 100 euro)
    [TestCase(99.00, 25.00, 124.00)]
    [TestCase(100.00, 25.00, 125.00)] // Exact 100 = nog verzending
    [TestCase(101.00, 0.00, 101.00)]  // Boven 100 = gratis verzending
    public void CalculateTotal_Verzendkosten_BerekendVolgensGrensVan100Euro(double productPrijs, double verwachteVerzendkosten, double verwachteTotaalPrijs)
    {
        var producten = new List<BesteldProductDto>
        {
            new BesteldProductDto { ProductNaam = "TestProduct", ProductPrijs = (decimal)productPrijs, Aantal = 1 }
        };

        BestellingCreatedDto result = classUnderTest.Calculate(producten, accountLeeftijd: 2);

        Assert.That(result.Verzendkosten, Is.EqualTo((decimal)verwachteVerzendkosten));
        Assert.That(result.TotaalPrijs, Is.EqualTo((decimal)verwachteTotaalPrijs));
    }

    // Rule: Korting bij account > 5 jaar
    [TestCase(5, 125.00)] // Exact 5 jaar = geen korting (100 + 25)
    [TestCase(6, 120.00)] // Ouder dan 5 jaar = 5% korting (95 + 25 verzending)
    public void CalculateTotal_AccountOuderDan5Jaar_Krijgt5ProcentKorting(int accountLeeftijd, double verwachteTotaalPrijs)
    {
        var producten = new List<BesteldProductDto>
        {
            new BesteldProductDto { ProductNaam = "Koptelefoon", ProductPrijs = 100.00m, Aantal = 1 }
        };

        BestellingCreatedDto result = classUnderTest.Calculate(producten, accountLeeftijd);

        Assert.That(result.TotaalPrijs, Is.EqualTo((decimal)verwachteTotaalPrijs));
    }

    // Rule: Korting bij bestelling > 500 euro
    [TestCase(500.00, 500.00)] // Exact 500 = geen korting (gratis verzending)
    [TestCase(501.00, 475.95)] // > 500 = 5% korting op 501.00 = 475.95
    public void CalculateTotal_ProductWaardeBoven500Euro_Krijgt5ProcentKorting(double productPrijs, double verwachteTotaalPrijs)
    {
        var producten = new List<BesteldProductDto>
        {
            new BesteldProductDto { ProductNaam = "TV", ProductPrijs = (decimal)productPrijs, Aantal = 1 }
        };

        BestellingCreatedDto result = classUnderTest.Calculate(producten, accountLeeftijd: 2);

        Assert.That(result.TotaalPrijs, Is.EqualTo((decimal)verwachteTotaalPrijs));
    }

    // =========================================================================
    // NIVEAU 3: Precisie & Afrondingslogica
    // =========================================================================

    [Test]
    public void CalculateTotal_UitkomstMetDrieDecimalen_RondtAltijdNaarBovenAf()
    {
        // 5% korting op 105.55 = 100.2725 -> moet naar boven afgerond worden op 100.28
        var producten = new List<BesteldProductDto>
        {
            new BesteldProductDto { ProductNaam = "Test", ProductPrijs = 105.55m, Aantal = 1 }
        };

        BestellingCreatedDto result = classUnderTest.Calculate(producten, accountLeeftijd: 6);

        Assert.That(result.TotaalPrijs, Is.EqualTo(100.28m));
    }

    // =========================================================================
    // NIVEAU 4: Gecombineerde Business Rules (Multi-condition / Interaction)
    // =========================================================================

    [Test]
    public void CalculateTotal_KlantOuderDan5JaarEnBestellingBoven500_PastBeideKortingenToe()
    {
        // Test de combinatie van 2 kortingsregels tegelijk (5% + 5% = 10% korting)
        var producten = new List<BesteldProductDto>
        {
            new BesteldProductDto { ProductNaam = "High-end Laptop", ProductPrijs = 1000.00m, Aantal = 1 }
        };

        BestellingCreatedDto result = classUnderTest.Calculate(producten, accountLeeftijd: 6);

        Assert.That(result.TotaalPrijs, Is.EqualTo(900.00m));
    }
}