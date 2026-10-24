using CatalogService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Persistence
{
    public class CatalogDbContext: DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options): base(options)
        {
            
        }
        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Product>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Product>()
                .Property(x => x.Price)
                .HasColumnType("money");
        }
    }
}
