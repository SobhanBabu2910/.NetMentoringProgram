using Microsoft.EntityFrameworkCore;
using RestfulCatalog.Application.Interfaces.Repositories;
using RestfulCatalog.Application.Models;
using RestfulCatalog.Domain.Entities;
using RestfulCatalog.Infrastructure.Persistence;

namespace RestfulCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products
                .AnyAsync(x => x.Id == id);
        }

        public async Task<PagedResult<Product>> GetPagedAsync(
            int? categoryId,
            int page,
            int pageSize)
        {
            IQueryable<Product> query =
                _context.Products
                    .Include(x => x.Category)
                    .AsNoTracking();

            if (categoryId.HasValue)
            {
                query = query.Where(
                    x => x.CategoryId == categoryId);
            }

            int totalRecords =
                await query.CountAsync();

            List<Product> products =
                await query
                    .OrderBy(x => x.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return new PagedResult<Product>
            {
                Items = products,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
