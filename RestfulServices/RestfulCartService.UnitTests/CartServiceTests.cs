using FluentAssertions;
using Moq;
using RestfulCartService.Application.Interfaces;
using RestfulCartService.Domain.Entities;
using RestfulCartService.Domain.Entities.DTOs;

namespace RestfulCartService.UnitTests
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<ICartRepository> _repositoryMock;

        private Application.Services.CartService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock =new Mock<ICartRepository>();

            _service =new Application.Services.CartService(
                    _repositoryMock.Object);
        }
        [Test]
        public async Task GetCartAsync_GivenExistingCart_WhenCalled_ThenReturnsCartDto()
        {
            var cart = new Cart
            {
                CartKey = "CART001",
                Items =
                [
                    new CartItem
                {
                    Id = 1,
                    ProductName = "Laptop",
                    Price = 50000,
                    Quantity = 1
                }
                ]
            };

            _repositoryMock.Setup(x =>x.GetCartAsync("CART001")).ReturnsAsync(cart);

            var result =await _service.GetCartAsync("CART001");

            result.Should().NotBeNull();

            result!.CartKey.Should().Be("CART001");
        }

        [Test]
        public async Task AddItemAsync_GivenValidRequest_WhenCalled_ThenRepositoryShouldBeCalled()
        {
            var request =
                new AddCartItemRequest
                {
                    ProductName = "Laptop",
                    Price = 50000,
                    Quantity = 1
                };

            await _service.AddItemAsync("CART001", request);

            _repositoryMock.Verify( x => x.AddItemAsync("CART001",It.IsAny<CartItem>()),
                                    Times.Once);

            _repositoryMock.Verify(x => x.SaveChangesAsync(),Times.Once);
        }

        [Test]
        public async Task DeleteItemAsync_GivenValidItem_WhenCalled_ThenRepositoryShouldBeCalled()
        {
            _repositoryMock.Setup(x => x.DeleteItemAsync("CART001", 1))
                .ReturnsAsync(true);

            await _service.DeleteItemAsync("CART001",1);

            _repositoryMock.Verify(x => x.DeleteItemAsync("CART001",1),Times.Once);

            _repositoryMock.Verify(x => x.SaveChangesAsync(),Times.Once);
        }

        [Test]
        public void DeleteItemAsync_GivenInvalidItem_WhenItemNotFound_ThenThrowsException()
        {
            _repositoryMock.Setup(x => x.DeleteItemAsync("CART001", 99))
                .ReturnsAsync(false);

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () =>
                {
                    await _service.DeleteItemAsync("CART001",99);
                });
        }
    }
}
