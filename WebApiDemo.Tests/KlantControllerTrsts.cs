using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiDemo.Controllers;
using WebApiDemo.Data;
using WebApiDemo.DTOs.Klanten;
using WebApiDemo.Models;
using WebApiDemo.Repositories;

namespace WebApiDemo.Tests;

[TestFixture]
public class KlantControllerTests
{
    private Mock<IUnitOfWork> _uowMock = null!;
    private Mock<IKlantRepository> _klantRepoMock = null!;
    private KlantController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _klantRepoMock = new Mock<IKlantRepository>();

        _uowMock.Setup(u => u.KlantRepository).Returns(_klantRepoMock.Object);
        _controller = new KlantController(_uowMock.Object, Mock.Of<ILogger<KlantController>>());
    }

    [Test]
    public async Task GetKlant_Bestaat_Retourneert200OkMetDto()
    {
        // ARRANGE
        var klant = new Klant { Id = 1, Naam = "Peeters", Voornaam = "Jan" };
        _klantRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(klant);

        // ACT
        ActionResult<KlantDto> result = await _controller.GetKlant(1);

        // ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.StatusCode, Is.EqualTo(200));

        var dto = okResult.Value as KlantDto;
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto!.Naam, Is.EqualTo("Peeters"));
    }

    [Test]
    public async Task GetKlant_BestaatNiet_Retourneert404NotFound()
    {
        // ARRANGE
        _klantRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Klant?)null);

        // ACT
        ActionResult<KlantDto> result = await _controller.GetKlant(99);

        // ASSERT
        Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task PostKlant_GeldigeData_Retourneert201CreatedEnSlaatOp()
    {
        // ARRANGE
        var dto = new KlantWriteDto { Naam = "Janssens", Voornaam = "An" };
        _klantRepoMock.Setup(r => r.BestaatKlantAsync(dto.Naam, dto.Voornaam)).ReturnsAsync(false);

        // ACT
        ActionResult<KlantDto> result = await _controller.PostKlant(dto);

        // ASSERT
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        _klantRepoMock.Verify(r => r.Add(It.IsAny<Klant>()), Times.Once);
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task PostKlant_KlantBestaatAl_RetourneertValidationProblem()
    {
        // ARRANGE
        var dto = new KlantWriteDto { Naam = "Janssens", Voornaam = "An" };
        // Simuleer dat de klant al bestaat in de database
        _klantRepoMock.Setup(r => r.BestaatKlantAsync(dto.Naam, dto.Voornaam)).ReturnsAsync(true);

        // ACT
        var result = await _controller.PostKlant(dto);

        // ASSERT
        var objectResult = result.Result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult.Value, Is.InstanceOf<ValidationProblemDetails>());

        // Verifieer dat er NIETS is toegevoegd of opgeslagen
        _klantRepoMock.Verify(r => r.Add(It.IsAny<Klant>()), Times.Never);
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
    }

    [Test]
    public async Task DeleteKlant_Bestaat_VerwijdertEnRetourneert204NoContent()
    {
        // ARRANGE
        var klant = new Klant { Id = 1, Naam = "Peeters" };
        _klantRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(klant);

        // ACT
        IActionResult result = await _controller.DeleteKlant(1);

        // ASSERT
        Assert.That(result, Is.InstanceOf<NoContentResult>());
        _klantRepoMock.Verify(r => r.Delete(klant), Times.Once);
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}