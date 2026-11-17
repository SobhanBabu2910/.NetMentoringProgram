using RestfulCartService.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCartService.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDto?> GetCartAsync(string cartKey);

        Task<List<CartItemDto>> GetCartItemsAsync(string cartKey);

        Task AddItemAsync(string cartKey,AddCartItemRequest request);

        Task DeleteItemAsync(string cartKey,int itemId);
    }
}
