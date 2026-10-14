using CartService.DAL.Database;
using CartService.DAL.Entities;

namespace CartService.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly LiteDbContext _context;

        public CartRepository(LiteDbContext context)
        {
            _context = context;
        }
        public Task<Cart?> GetByIdAsync(string cartId)
        {
            var cart =_context.Carts.FindOne(x => x.CartId == cartId);

            return Task.FromResult(cart);
        }

        public Task SaveAsync(Cart cart)
        {
            var collection =_context.Carts;

            collection.Upsert(cart);

            return Task.CompletedTask;
        }
        public Task DeleteAsync(string cartId)
        {
            _context.Carts.DeleteMany(x => x.CartId == cartId);

            return Task.CompletedTask;
        }
    }
}
