using RestfulCartService.Application.Interfaces;
using RestfulCartService.Domain.Entities;
using RestfulCartService.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCartService.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<CartDto?> GetCartAsync(string cartKey)
        {
            var cart = await _repository.GetCartAsync(cartKey);

            if (cart == null)
                return null;

            return new CartDto
            {
                CartKey = cart.CartKey,
                Items = cart.Items.Select(x => new CartItemDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList()
            };
        }

        public async Task<List<CartItemDto>> GetCartItemsAsync(string cartKey)
        {
            var cart = await _repository.GetCartAsync(cartKey);

            if (cart == null)
                return [];

            return cart.Items.Select(x => new CartItemDto
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();
        }

        public async Task AddItemAsync(string cartKey, AddCartItemRequest request)
        {
            var item = new CartItem
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity
            };

            await _repository.AddItemAsync(cartKey, item);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(string cartKey, int itemId)
        {
            var deleted =
                await _repository.DeleteItemAsync(cartKey, itemId);

            if (!deleted)
                throw new KeyNotFoundException("Item not found");

            await _repository.SaveChangesAsync();
        }
    }
}
