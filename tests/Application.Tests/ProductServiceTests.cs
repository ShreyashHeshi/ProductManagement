using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsProducts()
    {
        // Arrange
        var mockRepo =
            new Mock<IProductRepository>();

        var mockUow =
            new Mock<IUnitOfWork>();

        mockRepo.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    ProductName = "Laptop"
                }
            });

        mockUow.Setup(x => x.Products)
            .Returns(mockRepo.Object);

        var service =
            new ProductService(mockUow.Object);

        // Act
        var result =
            await service.GetAllAsync();

        // Assert
        Assert.Single(result);
    }


    [Fact]
    public async Task CreateAsync_ShouldSaveProduct()
    {
        var mockRepo =
            new Mock<IProductRepository>();

        var mockUow =
            new Mock<IUnitOfWork>();

        mockUow.Setup(x => x.Products)
            .Returns(mockRepo.Object);

        var service =
            new ProductService(mockUow.Object);

        await service.CreateAsync(
            new CreateProductDto
            {
                ProductName = "Phone"
            });

        mockRepo.Verify(
            x => x.AddAsync(It.IsAny<Product>()),
            Times.Once);

        mockUow.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}