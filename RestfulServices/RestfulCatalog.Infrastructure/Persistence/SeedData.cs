using RestfulCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static List<Category> Categories =>
            new()
            {
            new Category
            {
                Id = 1,
                Name = "Electronics"
            },
            new Category
            {
                Id = 2,
                Name = "Furniture"
            }
            };

        public static List<Product> Products =>
            new()
            {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 60000,
                Amount = 10,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Mouse",
                Price = 1500,
                Amount = 20,
                CategoryId = 1
            }
            };
    }
}
