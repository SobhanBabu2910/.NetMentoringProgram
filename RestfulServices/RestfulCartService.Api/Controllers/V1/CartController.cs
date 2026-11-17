using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestfulCartService.Application.Interfaces;
using RestfulCartService.Domain.Entities.DTOs;

namespace RestfulCartService.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet("{cartKey}")]
        public async Task<IActionResult> Get(
            string cartKey)
        {
            var result = await _service.GetCartAsync(cartKey);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{cartKey}/items")]
        public async Task<IActionResult> AddItem(
            string cartKey,
            AddCartItemRequest request)
        {
            await _service.AddItemAsync(cartKey,request);

            return Ok();
        }

        [HttpDelete("{cartKey}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem(string cartKey,int itemId)
        {
            await _service.DeleteItemAsync(cartKey,itemId);

            return Ok();
        }
    }
}
