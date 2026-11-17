using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Interfaces;
using RestfulCartService.Application.Interfaces;

namespace RestfulCartService.Application.Consumers
{
    public class ProductUpdatedConsumer : IMessageConsumer<ProductUpdatedEvent>
    {
        private readonly ICartRepository _cartRepository;

        public ProductUpdatedConsumer(
            ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task HandleAsync(
        ProductUpdatedEvent message)
        {
            var carts =await _cartRepository.GetCartAsync(message.ProductId.ToString());

            if(carts is null) return;

            foreach (var cart in carts.Items.Where(x => x.Id == message.ProductId))
            {
                cart.ProductName = message.Name;
                cart.Price = message.Price;
            }
            await _cartRepository.SaveChangesAsync();
        }
    }
}
