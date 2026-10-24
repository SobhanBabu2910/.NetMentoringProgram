using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using CatalogService.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogService.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x =>
                new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Amount = x.Amount,
                    CategoryId = x.CategoryId
                });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Amount = product.Amount,
                CategoryId = product.CategoryId
            };
        }

        public async Task<Product> CreateAsync(Product dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ValidationException("Name required");

            if (dto.Name.Length > 50)
                throw new ValidationException("Max length 50");

            if (dto.Price <= 0)
                throw new ValidationException("Price required");

            if (dto.Amount <= 0)
                throw new ValidationException("Amount > 0 required");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Amount = dto.Amount,
                CategoryId = dto.CategoryId
            };

            await _repository.AddAsync(product);

            dto.Id = product.Id;

            return dto;
        }

        public async Task UpdateAsync(Product dto)
        {
            var entity =
                await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
                throw new Exception("Not Found");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Amount = dto.Amount;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
