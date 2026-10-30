using CartService.BLL.DTOs;
using CartService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var cart = await _service.GetCartAsync(cartId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("{cartId}/items")]
        public async Task<IActionResult> AddCartItem(string cartId, AddCartItemRequest request)
        {
            await _service.AddItemAsync(cartId, request);
            return Ok();
        }

        [HttpDelete("{cartId}/items/{itemId}")]
        public async Task<IActionResult> RemoveCartItem(string cartId, int itemId)
        {
            await _service.RemoveItemAsync(cartId, itemId);
            return Ok();
        }

    }
}
