using CartService.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.BLL.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> GetCartAsync(string cartId);

        Task AddItemAsync(string cartId,AddCartItemRequest request);

        Task RemoveItemAsync(string cartId,int itemId);
    }
}
