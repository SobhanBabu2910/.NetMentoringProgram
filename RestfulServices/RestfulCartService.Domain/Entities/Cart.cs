using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCartService.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public string CartKey { get; set; } = string.Empty;

        public ICollection<CartItem> Items { get; set; }
            = new List<CartItem>();
    }
}
