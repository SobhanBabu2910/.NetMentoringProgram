using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CatalogService.Persistence
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();

            // Try environment variables first, otherwise fall back to LocalDB for design-time
            // Below connection string configurations needs to be set in appsettings.json or environment variables for production use
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection")
                                   ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                   ?? "Server=(localdb)\\MSSQLLocalDB;Database=CatalogDb;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
