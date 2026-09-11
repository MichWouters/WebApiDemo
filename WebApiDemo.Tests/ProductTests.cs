using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiDemo.Controllers;
using WebApiDemo.Data;
using WebApiDemo.DTOs.Producten;
using WebApiDemo.Models;
using WebApiDemo.Repositories;

namespace WebApiDemo.Tests;

public class ProductControllerTests
{
    private Mock<IUnitOfWork> _uowMock = null!;
    private Mock<IGenericRepository<Product>> _productRepoMock = null!;
    private Mock<ILogger<ProductController>> _loggerMock = null!;
    private ProductController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _productRepoMock = new Mock<IGenericRepository<Product>>();
        _loggerMock = new Mock<ILogger<ProductController>>();

        // Koppel de product repository mock aan de Unit of Work mock
        _uowMock.Setup(u => u.ProductRepository).Returns(_productRepoMock.Object);

        _controller = new ProductController(_uowMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetProduct_ProductBestaat_Retourneert200OkMetProductDto()
    {
        // ARRANGE
        int testId = 1;
        var bestaandProduct = new Product { Id = testId, Naam = "Mechanisch Toetsenbord", Prijs = 89.99m };
        _productRepoMock.Setup(r => r.GetByIdAsync(testId)).ReturnsAsync(bestaandProduct);

        // ACT
        var result = await _controller.GetProduct(testId);

        // ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.StatusCode, Is.EqualTo(200));

        var dto = okResult.Value as ProductDto;
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto!.Naam, Is.EqualTo("Mechanisch Toetsenbord"));
        Assert.That(dto.Prijs, Is.EqualTo(89.99m));
    }

    [Test]
    public async Task GetProduct_ProductBestaatNiet_Retourneert404NotFound()
    {
        // ARRANGE
        int testId = 99;
        _productRepoMock.Setup(r => r.GetByIdAsync(testId)).ReturnsAsync((Product?)null);

        // ACT
        var result = await _controller.GetProduct(testId);

        // ASSERT
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.That(notFoundResult, Is.Not.Null);
        Assert.That(notFoundResult!.StatusCode, Is.EqualTo(404));
    }

    [Test]
    public async Task PostProduct_GeldigeData_VoegtToeEnRetourneert201Created()
    {
        // ARRANGE
        var dto = new ProductWriteDto { Naam = "Ergonomische Muis", Prijs = 35.00m };

        // ACT
        var result = await _controller.PostProduct(dto);

        // ASSERT
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult, Is.Not.Null);
        Assert.That(createdResult!.StatusCode, Is.EqualTo(201));

        // Verifieer dat Add en SaveChangesAsync exact 1x zijn aangeroepen
        _productRepoMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task PutProduct_ProductBestaatNiet_Retourneert404EnSlaatNietOp()
    {
        // ARRANGE
        int testId = 99;
        var dto = new ProductWriteDto { Naam = "Update Naam", Prijs = 10.00m };
        _productRepoMock.Setup(r => r.GetByIdAsync(testId)).ReturnsAsync((Product?)null);

        // ACT
        var result = await _controller.PutProduct(testId, dto);

        // ASSERT
        var notFoundResult = result as NotFoundObjectResult;
        Assert.That(notFoundResult, Is.Not.Null);
        Assert.That(notFoundResult!.StatusCode, Is.EqualTo(404));

        // Verifieer dat er niets is opgeslagen in de database
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
    }

    [Test]
    public async Task DeleteProduct_ProductBestaat_VerwijdertEnRetourneert204NoContent()
    {
        // ARRANGE
        int testId = 1;
        var bestaandProduct = new Product { Id = testId, Naam = "Monitor" };
        _productRepoMock.Setup(r => r.GetByIdAsync(testId)).ReturnsAsync(bestaandProduct);

        // ACT
        var result = await _controller.DeleteProduct(testId);

        // ASSERT
        Assert.That(result, Is.InstanceOf<NoContentResult>());
        _productRepoMock.Verify(r => r.Delete(bestaandProduct), Times.Once);
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}