using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.BLL.DTOs
{
    public class CartResponse
    {
        public string CartId { get; set; } = string.Empty;

        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
    }
}
