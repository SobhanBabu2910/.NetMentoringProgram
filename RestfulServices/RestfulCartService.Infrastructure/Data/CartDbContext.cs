using RestfulCartService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RestfulCartService.Infrastructure.Data
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts => Set<Cart>();

        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(
            ModelBuilder builder)
        {
            builder.Entity<Cart>()
                .HasIndex(x => x.CartKey)
                .IsUnique();

            builder.Entity<Cart>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Cart)
                .HasForeignKey(x => x.CartId);
        }
    }
}
