using WebApiDemo.Controllers;

namespace WebApiDemo.Tests;

public class WelkomControllerTests
{
    [Test]
    public void GetWelkomBericht_RetourneertJuisteStandaardBericht()
    {
        // Arrange
        var controller = new WelkomController();

        // Act
        var result = controller.GetWelkomBericht();

        // Assert
        Assert.That(result.Value, Is.EqualTo("Welkom bij Programming Advanced! Je eerste eigen endpoint werkt 🎉"));
    }

    [TestCase("Jan", "Welkom bij Programming Advanced, Jan! 🚀")]
    [TestCase("An", "Welkom bij Programming Advanced, An! 🚀")]
    public void GetGepersonaliseerdBericht_MetNaam_RetourneertGepersonaliseerdBericht(string naam, string verwachteTekst)
    {
        // Arrange
        var controller = new WelkomController();

        // Act
        var result = controller.GetGepersonaliseerdBericht(naam);

        // Assert
        Assert.That(result.Value, Is.EqualTo(verwachteTekst));
    }
}