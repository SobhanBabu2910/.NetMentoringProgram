using System;
using System.IO;
using System.Threading.Tasks;
using CartService.DAL.Database;
using CartService.DAL.Repositories;
using CartService.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;

namespace CartService.Tests.Repositories
{
    [TestFixture]
    public class CartRepositoryTests
    {
        private string _dbPath = string.Empty;
        private LiteDbContext _context = null!;
        private CartRepository _sut = null!;

        [SetUp]
        public void Setup()
        {
            _dbPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".db");
            _context = new LiteDbContext(_dbPath);
            _sut = new CartRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (File.Exists(_dbPath)) File.Delete(_dbPath);
            }
            catch
            {
                // ignore
            }
        }

        [Test]
        public async Task GetByIdAsync_GivenCartExists_WhenCalled_ThenReturnCart()
        {
            // Arrange
            var cart = CartBuilder.Create();
            _context.Carts.Insert(cart);

            // Act
            var result = await _sut.GetByIdAsync(cart.CartId);

            // Assert
            result.Should().NotBeNull();
            result!.CartId.Should().Be(cart.CartId);
        }

        [Test]
        public async Task SaveAsync_GivenCart_WhenSaved_ThenPersist()
        {
            // Arrange
            var cart = CartBuilder.Create();
            cart.Items.Add(CartBuilder.CreateItem());

            // Act
            await _sut.SaveAsync(cart);

            // Assert
            var stored = _context.Carts.FindOne(x => x.CartId == cart.CartId);
            stored.Should().NotBeNull();
            stored!.Items.Should().HaveCount(1);
        }

        [Test]
        public async Task DeleteAsync_GivenCartExists_WhenDeleted_ThenRemoved()
        {
            // Arrange
            var cart = CartBuilder.Create();
            _context.Carts.Insert(cart);

            // Act
            await _sut.DeleteAsync(cart.CartId);

            // Assert
            var stored = _context.Carts.FindOne(x => x.CartId == cart.CartId);
            stored.Should().BeNull();
        }
    }
}
