using CartService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.DAL.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(string cartId);

        Task SaveAsync(Cart cart);

        Task DeleteAsync(string cartId);
    }
}
