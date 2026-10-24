using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
