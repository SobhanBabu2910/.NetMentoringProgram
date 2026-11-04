using RestfulCatalog.Application.DTOs.Product;

namespace RestfulCatalog.Application.Models
{
    public class ProductResponse
    {
        public ProductDto Product { get; set; } = null!;

        public List<LinkDto> Links { get; set; } = new();
    }
}
