using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.BLL.DTOs
{
    public class AddCartItemRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string AltText { get; set; } = string.Empty;
    }
}
