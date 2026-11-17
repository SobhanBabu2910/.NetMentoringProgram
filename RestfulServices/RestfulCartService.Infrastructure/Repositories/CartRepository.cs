using RestfulCartService.Application.Interfaces;
using RestfulCartService.Domain.Entities;
using RestfulCartService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace RestfulCartService.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(
            CartDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartAsync(
            string cartKey)
        {
            return await _context.Carts.Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.CartKey == cartKey);
        }

        public async Task AddItemAsync(string cartKey,CartItem item)
        {
            var cart = await GetCartAsync(cartKey);

            if (cart is null)
            {
                cart = new Cart
                {
                    CartKey = cartKey
                };

                _context.Carts.Add(cart);
            }

            cart.Items.Add(item);
        }

        public async Task<bool> DeleteItemAsync(string cartKey,int itemId)
        {
            var cart = await GetCartAsync(cartKey);

            if (cart == null)
                return false;

            var item =cart.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                return false;

            _context.CartItems.Remove(item);

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
