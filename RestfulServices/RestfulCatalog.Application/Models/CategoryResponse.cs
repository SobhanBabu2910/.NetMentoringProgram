using RestfulCatalog.Application.DTOs.Category;

namespace RestfulCatalog.Application.Models
{
    public class CategoryResponse
    {
        public CategoryDto Category { get; set; } = null!;

        public List<LinkDto> Links { get; set; } = new();
    }
}
