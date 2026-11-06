using RestfulCatalog.Application.Models;
using RestfulCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<PagedResult<Product>> GetPagedAsync(
        int? categoryId,
        int page,
        int pageSize);

        Task<Product?> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
