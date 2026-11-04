using RestfulCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(Category category);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
