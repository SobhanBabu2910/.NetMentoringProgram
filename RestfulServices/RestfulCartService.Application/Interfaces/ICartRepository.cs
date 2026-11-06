using RestfulCartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCartService.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(string cartKey);

        Task AddItemAsync(string cartKey, CartItem item);

        Task<bool> DeleteItemAsync(string cartKey, int itemId);

        Task SaveChangesAsync();
    }
}
