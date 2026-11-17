using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCartService.Domain.Entities.DTOs
{
    public class CartDto
    {
        public string CartKey { get; set; } = string.Empty;

        public List<CartItemDto> Items { get; set; } = new();
    }
}
