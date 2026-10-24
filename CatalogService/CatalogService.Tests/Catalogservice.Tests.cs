using CatalogService.Application.Interfaces;
using CatalogService.Application.Service;
using CatalogService.Domain;
using Moq;

namespace CatalogService.Tests
{
    public class CatalogserviceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        
        private ProductService _productService;
        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Test]
        public async Task AddAsync_GivenValidProduct_WhenAddProductCalled_ThenRepositoryShouldBeCalled()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000,
                Amount = 5,
                CategoryId = 1
            };

            _productRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);


            await _productService.CreateAsync(product);

            _productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Product>()),
                Times.Once);
        }
       
        [Test]
        public async Task GetByIdAsync_GivenExistingProduct_WhenGetByIdCalled_ThenProductShouldBeReturned()
        {
            var expectedProduct = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000,
                Amount = 10,
                CategoryId = 1
            };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(expectedProduct);

            var result = await _productService.GetByIdAsync(1);

            Assert.That(result, Is.Not.Null);

            Assert.That(result!.Id, Is.EqualTo(1));

            Assert.That(result.Name, Is.EqualTo("Laptop"));
        }
        [Test]
        public async Task GetByIdAsync_GivenNonExistingProduct_WhenGetByIdCalled_ThenNullShouldBeReturned()
        {
            _productRepositoryMock.Setup(x => x.GetByIdAsync(100)).ReturnsAsync((Product?)null);

            var result = await _productService.GetByIdAsync(100);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_GivenValidProductId_WhenDeleteCalled_ThenRepositoryShouldBeCalled()
        {
            const int productId = 1;

            await _productService.DeleteAsync(productId);

            _productRepositoryMock.Verify(x => x.DeleteAsync(productId),Times.Once);
        }

        [Test]
        public async Task UpdateAsync_GivenValidProduct_WhenUpdateCalled_ThenRepositoryShouldBeCalled()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Gaming Laptop",
                Price = 65000,
                Amount = 20,
                CategoryId = 2
            };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(product.Id))
                .ReturnsAsync(product);

            _productRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            await _productService.UpdateAsync(product);

            _productRepositoryMock.Verify(
                x => x.UpdateAsync(
                    It.Is<Product>(p =>
                        p.Id == 1 &&
                        p.Name == "Gaming Laptop" &&
                        p.Price == 65000 &&
                        p.Amount == 20 &&
                        p.CategoryId == 2)),
                Times.Once);
        }

        [Test]
        public async Task GetAllAsync_GivenProductsExist_WhenGetAllCalled_ThenProductsShouldBeReturned()
        {
            var products = new List<Product>
            {
                new()
                {
                    Id = 1,
                    Name = "Laptop"
                },
                new()
                {
                    Id = 2,
                    Name = "Mobile"
                }
            };

            _productRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            var result = await _productService.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}
