using Microsoft.EntityFrameworkCore;
using RestfulCatalog.Application.Interfaces.Repositories;
using RestfulCatalog.Domain.Entities;
using RestfulCatalog.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDbContext _context;

        public CategoryRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories
                .AnyAsync(x => x.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
