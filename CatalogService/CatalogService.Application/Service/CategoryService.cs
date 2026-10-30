using CatalogService.Application.Interfaces;
using CatalogService.Domain;

namespace CatalogService.Application.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task AddAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException(
                    "Category Name is required");
            }

            if (category.Name.Length > 50)
            {
                throw new ArgumentException(
                    "Category Name cannot exceed 50 characters");
            }

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            var existing =
                await _categoryRepository.GetByIdAsync(category.Id);

            if (existing == null)
            {
                throw new KeyNotFoundException(
                    $"Category {category.Id} not found");
            }

            existing.Name = category.Name;
            existing.ImageUrl = category.ImageUrl;
            existing.ParentCategoryId =
                category.ParentCategoryId;

            await _categoryRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var category =
                await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException(
                    $"Category {id} not found");
            }

            await _categoryRepository.DeleteAsync(id);
        }
    }
}
