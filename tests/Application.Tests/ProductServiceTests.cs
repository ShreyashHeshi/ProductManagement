using Application.Interfaces;
using Application.Services;
using Moq;
using Domain.Entities;
using Xunit;
using Application.DTOs;

public class ProductServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsProducts()
    {
        var repository =
            new Mock<IProductRepository>();

        repository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    ProductName = "Laptop"
                }
            });

        var uow = new Mock<IUnitOfWork>();

        uow.Setup(x => x.Products)
            .Returns(repository.Object);

        var service =
            new ProductService(uow.Object);

        var result =
            await service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct()
    {
        var repository =
            new Mock<IProductRepository>();

        repository.Setup(x =>
            x.GetByIdAsync(1))
            .ReturnsAsync(
                new Product
                {
                    Id = 1,
                    ProductName = "Mouse"
                });

        var uow = new Mock<IUnitOfWork>();

        uow.Setup(x => x.Products)
            .Returns(repository.Object);

        var service =
            new ProductService(uow.Object);

        var result =
            await service.GetByIdAsync(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateAsync_ReturnsId()
    {
        var repository =
            new Mock<IProductRepository>();

        var uow =
            new Mock<IUnitOfWork>();

        uow.Setup(x => x.Products)
            .Returns(repository.Object);

        uow.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var service =
            new ProductService(uow.Object);

        var result =
            await service.CreateAsync(
                new CreateProductDto
                {
                    ProductName = "Keyboard"
                });

        Assert.True(result >= 0);
    }
}