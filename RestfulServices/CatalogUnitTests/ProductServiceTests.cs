using Moq;
using RestfulCatalog.Application.DTOs.Product;
using RestfulCatalog.Application.Interfaces.Repositories;
using RestfulCatalog.Application.Interfaces.Services;
using RestfulCatalog.Application.Services;
using RestfulCatalog.Domain.Entities;

namespace CatalogUnitTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private ProductService _productService;


        [SetUp]
        public void Setup()
        {
            _productRepositoryMock =
            new Mock<IProductRepository>();

            _categoryRepositoryMock =
                new Mock<ICategoryRepository>();

            _productService = new ProductService(
                _productRepositoryMock.Object,
                _categoryRepositoryMock.Object);
        }
        [Test]
        public async Task CreateAsync_GivenValidProductDto_WhenCalled_ThenProductShouldBeCreated()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Electronics"
            };

            var dto = new ProductDto(1,"Laptop",50000,10,1);

            _categoryRepositoryMock
                .Setup(x => x.GetByIdAsync(dto.CategoryId))
                .ReturnsAsync(category);

            _productRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            await _productService.CreateAsync(dto);

            _productRepositoryMock.Verify(
                x => x.AddAsync(
                    It.Is<Product>(p =>
                        p.Name == dto.Name &&
                        p.Price == dto.Price &&
                        p.Amount == dto.Amount &&
                        p.CategoryId == dto.CategoryId)),
                Times.Once);
        }

        [Test]
        public async Task UpdateAsync_GivenValidProduct_WhenCalled_ThenRepositoryShouldBeCalled()
        {
            int productId = 1;

            var existingProduct = new Product
            {
                Id = 1,
                Name = "Old Laptop",
                Price = 40000,
                Amount = 5,
                CategoryId = 1
            };

            var category = new Category
            {
                Id = 1,
                Name = "Electronics"
            };

            var dto = new UpdateProductDto("Laptop", 50000, 10);

            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            _categoryRepositoryMock
                .Setup(x => x.GetByIdAsync(category.Id))
                .ReturnsAsync(category);

            _productRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            await _productService.UpdateAsync(productId,dto);

            _productRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Product>(p =>
                        p.Id == productId &&
                        p.Name == dto.Name &&
                        p.Price == dto.Price &&
                        p.Amount == dto.Amount &&
                        p.CategoryId == category.Id)),
                Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_GivenExistingProduct_WhenCalled_ThenShouldReturnProduct()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000,
                Amount = 10,
                CategoryId = 1
            };

            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            var result =
                await _productService.GetByIdAsync(1);

            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public async Task DeleteAsync_GivenValidProductId_WhenCalled_ThenRepositoryShouldDelete()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Laptop"
            };

            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            _productRepositoryMock
                .Setup(x => x.DeleteAsync(product))
                .Returns(Task.CompletedTask);

            await _productService.DeleteAsync(1);

            _productRepositoryMock.Verify(
                x => x.DeleteAsync(product),
                Times.Once);
        }
    }
}
