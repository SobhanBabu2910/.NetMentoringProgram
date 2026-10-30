using CartService.BLL.DTOs;
using CartService.Tests.Builders;

namespace CartService.Tests.Services
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<ICartRepository> _repositoryMock;
        private CartService.BLL.Services.CartService _sut;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<ICartRepository>();

            _sut = new CartService.BLL.Services.CartService(
                _repositoryMock.Object);
        }

        [Test]
        public async Task GetItemsAsync_GivenCartExists_WhenCalled_ThenReturnItems()
        {
            // Arrange

            var cartId = Guid.NewGuid().ToString();

            var cart = CartBuilder.Create();

            cart.Items.Add(
                CartBuilder.CreateItem());

            _repositoryMock
                .Setup(x => x.GetByIdAsync(cartId))
                .ReturnsAsync(cart);

            // Act
            var result = await _sut.GetCartAsync(cartId);

            // Assert
            _repositoryMock.Verify( x => x.GetByIdAsync(cartId),Times.Once);
        }

        [Test]
        public async Task AddItemAsync_GivenValidCart_WhenAddingItemRequest_ThenSaveCart()
        {
            // Arrange
            var cartId = Guid.NewGuid().ToString();
            var cart = CartBuilder.Create();
            _repositoryMock.Setup(x => x.GetByIdAsync(cartId)).ReturnsAsync(cart);

            var dalItem = CartBuilder.CreateItem();

            var request = new AddCartItemRequest
            {
                Name = dalItem.Name,
                Price = dalItem.Price,
                Quantity = dalItem.Quantity,
            };

            // Act
            await _sut.AddItemAsync(cartId, request);

            // Assert
            cart.Items.Should().ContainSingle();
            _repositoryMock.Verify( x => x.SaveAsync(It.IsAny<Cart>()),Times.Once);
        }
        [Test]
        public async Task RemoveItemAsync_GivenItemExists_WhenRemoved_ThenDeleteItem()
        {
            // Arrange

            var cartId = Guid.NewGuid().ToString();

            var item = CartBuilder.CreateItem();

            var cart = CartBuilder.Create();

            cart.Items.Add(item);

            _repositoryMock
                .Setup(x => x.GetByIdAsync(cartId))
                .ReturnsAsync(cart);

            // Act
            await _sut.RemoveItemAsync(cartId,item.Id);

            // Assert
            cart.Items.Should().BeEmpty();
        }
    }
}
