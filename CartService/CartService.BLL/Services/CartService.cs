using CartService.BLL.DTOs;
using CartService.BLL.Interfaces;
using CartService.DAL.Entities;
using CartService.DAL.Repositories;

namespace CartService.BLL.Services
{
    public class CartService: ICartService
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<CartResponse> GetCartAsync(string cartId)
        {
            var cart = await _repository.GetByIdAsync(cartId);

            if (cart == null)
            {
                return new CartResponse
                {
                    CartId = cartId,
                    Items = new List<CartItemResponse>()
                };
            }

            return new CartResponse
            {
                CartId = cart.CartId,

                Items = cart.Items.Select(x =>
                    new CartItemResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Quantity = x.Quantity
                    }).ToList()
            };
        }

        public async Task AddItemAsync(string cartId,AddCartItemRequest request)
        {
            var cart = await _repository.GetByIdAsync(cartId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = cartId
                };
            }

            var item = cart.Items.FirstOrDefault(x => x.Id == request.Id);

            if (item != null)
            {
                item.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    Id = request.Id,
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity,

                    Image = new CartImage
                    {
                        Url = request.ImageUrl,
                        AltText = request.AltText
                    }
                });
            }

            await _repository.SaveAsync(cart);
        }
        public async Task RemoveItemAsync(string cartId,int itemId)
        {
            var cart = await _repository.GetByIdAsync(cartId);

            if (cart == null)
                return;

            cart.Items.RemoveAll(x => x.Id == itemId);

            await _repository.SaveAsync(cart);
        }
    }
}
