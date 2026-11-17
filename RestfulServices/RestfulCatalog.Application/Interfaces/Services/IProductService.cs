using RestfulCatalog.Application.DTOs.Product;
using RestfulCatalog.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetPagedAsync(
        int? categoryId,
        int page,
        int pageSize);

        Task<ProductResponse?> GetByIdAsync(int id);

        Task<int> CreateAsync(
            ProductDto dto);

        Task UpdateAsync(
            int id,
            UpdateProductDto dto);

        Task DeleteAsync(int id);
    }
}
