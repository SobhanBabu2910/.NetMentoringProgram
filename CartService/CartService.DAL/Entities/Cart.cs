using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.DAL.Entities
{
    public class Cart
    {
        [BsonId]
        public string CartId { get; set; } = string.Empty;

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
