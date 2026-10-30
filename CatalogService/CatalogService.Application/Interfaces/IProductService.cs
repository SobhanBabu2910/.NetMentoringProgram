using CatalogService.Application.Dto;
using CatalogService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto?> GetByIdAsync(int id);

        Task<Product> CreateAsync(Product dto);

        Task UpdateAsync(Product dto);

        Task DeleteAsync(int id);
    }
}
