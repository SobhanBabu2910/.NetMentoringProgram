using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestfulCartService.Application.Interfaces;
using RestfulCartService.Domain.Entities.DTOs;

namespace RestfulCartService.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cart")]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        /// <summary>
        /// Returns only cart items.
        /// </summary>
        /// <param name="cartKey">Unique cart key.</param>
        /// <returns>List of cart items.</returns>
        [HttpGet("{cartKey}")]
        [ProducesResponseType(typeof(List<CartItemDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItems(string cartKey)
        {
            var items =await _cartService.GetCartItemsAsync(cartKey);

            return Ok(items);
        }

    }
}
