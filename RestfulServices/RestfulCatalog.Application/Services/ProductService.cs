using RestfulCatalog.Application.DTOs.Product;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResult<ProductDto>>
            GetPagedAsync(
            int? categoryId,
            int page,
            int pageSize)
        {
            var result =
                await _productRepository
                    .GetPagedAsync(
                        categoryId,
                        page,
                        pageSize);

            return new PagedResult<ProductDto>
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords,

                Items = result.Items.Select(x =>
                    new ProductDto(
                        x.Id,
                        x.Name,
                        x.Price,
                        x.Amount,
                        x.CategoryId))
            };
        }

        public async Task<ProductResponse?>
            GetByIdAsync(int id)
        {
            var product =
                await _productRepository
                    .GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductResponse
            {
                Product = new ProductDto(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Amount,
                    product.CategoryId),

                Links = new List<LinkDto>()
            {
                new()
                {
                    Href=$"/api/v1/products/{id}",
                    Rel="self",
                    Method="GET"
                },
                new()
                {
                    Href=$"/api/v1/products/{id}",
                    Rel="update",
                    Method="PUT"
                },
                new()
                {
                    Href=$"/api/v1/products/{id}",
                    Rel="delete",
                    Method="DELETE"
                }
            }
            };
        }

        public async Task<int>
            CreateAsync(ProductDto dto)
        {
            var categoryExists =
                await _categoryRepository
                    .ExistsAsync(dto.CategoryId);

            if (!categoryExists)
            {
                throw new ValidationException(
                    "Invalid category id");
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Amount = dto.Amount,
                CategoryId = dto.CategoryId
            };

            await _productRepository
                .AddAsync(product);

            await _productRepository
                .SaveChangesAsync();

            return product.Id;
        }

        public async Task UpdateAsync(
            int id,
            UpdateProductDto dto)
        {
            var product =
                await _productRepository
                    .GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException(
                    "Product not found");
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Amount = dto.Amount;

            await _productRepository
                .UpdateAsync(product);

            await _productRepository
                .SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product =
                await _productRepository
                    .GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException(
                    "Product not found");
            }

            await _productRepository
                .DeleteAsync(product);

            await _productRepository
                .SaveChangesAsync();
        }
    }
}
