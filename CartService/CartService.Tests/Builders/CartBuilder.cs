using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.Tests.Builders
{
    public class CartBuilder
    {
        public static Cart Create()
        {
            return new Cart
            {
                CartId = Guid.NewGuid().ToString(),
                Items = new List<CartItem>()
            };
        }

        public static CartItem CreateItem(
            int id = 1,
            string name = "Laptop",
            decimal price = 1000,
            int quantity = 1)
        {
            return new CartItem
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity
            };
        }
    }
}
