using RestfulCatalog.Application.DTOs.Category;
using RestfulCatalog.Application.Exceptions;
using RestfulCatalog.Application.Interfaces.Repositories;
using RestfulCatalog.Application.Interfaces.Services;
using RestfulCatalog.Application.Models;
using RestfulCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>>
            GetAllAsync()
        {
            var categories =
                await _repository.GetAllAsync();

            return categories.Select(x =>
                new CategoryDto(
                    x.Id,
                    x.Name));
        }

        public async Task<CategoryResponse?>
            GetByIdAsync(int id)
        {
            var category =
                await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryResponse
            {
                Category =
                    new CategoryDto(
                        category.Id,
                        category.Name),

                Links = new List<LinkDto>
            {
                new()
                {
                    Href=$"/api/v1/categories/{id}",
                    Rel="self",
                    Method="GET"
                },
                new()
                {
                    Href=$"/api/v1/categories/{id}",
                    Rel="update",
                    Method="PUT"
                },
                new()
                {
                    Href=$"/api/v1/categories/{id}",
                    Rel="delete",
                    Method="DELETE"
                }
            }
            };
        }

        public async Task<int>
            CreateAsync(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ValidationException(
                    "Category name is required");
            }

            var category = new Category
            {
                Name = dto.Name
            };

            await _repository.AddAsync(category);

            await _repository.SaveChangesAsync();

            return category.Id;
        }

        public async Task UpdateAsync(
            int id,
            UpdateCategoryDto dto)
        {
            var category =
                await _repository.GetByIdAsync(id);

            if (category == null)
            {
                throw new NotFoundException(
                    "Category not found");
            }

            category.Name = dto.Name;

            await _repository.UpdateAsync(category);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category =
                await _repository.GetByIdAsync(id);

            if (category == null)
            {
                throw new NotFoundException(
                    "Category not found");
            }

            await _repository.DeleteAsync(category);

            await _repository.SaveChangesAsync();
        }
    }
}
