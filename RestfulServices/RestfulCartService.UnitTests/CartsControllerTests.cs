using Microsoft.AspNetCore.Mvc;
using Moq;
using RestfulCartService.Api.Controllers.V1;
using RestfulCartService.Application.Interfaces;
using RestfulCartService.Domain.Entities.DTOs;
using FluentAssertions;

namespace RestfulCartService.UnitTests
{
    [TestFixture]
    public class CartsControllerTests
    {
        private Mock<ICartService> _cartServiceMock;

        private CartController _controller;

        [SetUp]
        public void Setup()
        {
            _cartServiceMock = new Mock<ICartService>();

            _controller = new CartController(_cartServiceMock.Object);
        }


        #region GetCart

        [Test]
        public async Task GetCart_GivenValidCartKey_WhenCartExists_ThenReturnsOkResult()
        {
            var cartDto = new CartDto
            {
                CartKey = "CART001",
                Items =
                [
                    new CartItemDto
                {
                    Id = 1,
                    ProductName = "Laptop",
                    Price = 1000,
                    Quantity = 2
                }
                ]
            };

            _cartServiceMock.Setup(x => x.GetCartAsync("CART001"))
                .ReturnsAsync(cartDto);

            var result =await _controller.Get("CART001");

            result.Should().BeOfType<OkObjectResult>();

        }

        [Test]
        public async Task GetCart_GivenInvalidCartKey_WhenCartDoesNotExist_ThenReturnsNotFound()
        {
            _cartServiceMock.Setup(x => x.GetCartAsync("INVALID"))
                .ReturnsAsync((CartDto?)null);

            var result = await _controller.Get("INVALID");

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region AddItem

        [Test]
        public async Task AddItem_GivenValidRequest_WhenCalled_ThenReturnsOkResult()
        {
            var request = new AddCartItemRequest
            {
                ProductName = "Laptop",
                Price = 1000,
                Quantity = 2
            };

            _cartServiceMock.Setup(x => x.AddItemAsync("CART001",request))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddItem("CART001",request);

            result.Should().BeOfType<OkObjectResult>();

            _cartServiceMock.Verify(x => x.AddItemAsync(
                    "CART001",
                    request),
                Times.Once);
        }

        #endregion

        #region DeleteItem

        [Test]
        public async Task DeleteItem_GivenValidItemId_WhenItemExists_ThenReturnsOkResult()
        {
            _cartServiceMock.Setup(x => x.DeleteItemAsync("CART001",1))
                .Returns(Task.CompletedTask);

            var result =await _controller.DeleteItem("CART001",1);

            result.Should().BeOfType<OkObjectResult>();

            _cartServiceMock.Verify(x => x.DeleteItemAsync("CART001",1),Times.Once);
        }

        [Test]
        public void DeleteItem_GivenInvalidItemId_WhenItemDoesNotExist_ThenThrowsException()
        {
            _cartServiceMock.Setup(x => x.DeleteItemAsync("CART001",999))
                .ThrowsAsync(
                    new KeyNotFoundException());

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () =>
                {
                    await _controller.DeleteItem("CART001",999);
                });
        }

        #endregion
    }
}
