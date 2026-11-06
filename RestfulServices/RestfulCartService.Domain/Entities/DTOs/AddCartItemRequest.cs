using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCartService.Domain.Entities.DTOs
{
    public class AddCartItemRequest
    {
        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
