using RestfulCatalog.Application.DTOs.Category;
using RestfulCatalog.Application.Models;
using RestfulCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryResponse?> GetByIdAsync(int id);

        Task<int> CreateAsync(
            CreateCategoryDto dto);

        Task UpdateAsync(
            int id,
            UpdateCategoryDto dto);

        Task DeleteAsync(int id);
    }
}
