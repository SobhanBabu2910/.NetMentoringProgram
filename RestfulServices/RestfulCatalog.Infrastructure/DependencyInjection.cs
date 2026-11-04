using Microsoft.Extensions.DependencyInjection;
using RestfulCatalog.Application.Interfaces.Repositories;
using RestfulCatalog.Application.Interfaces.Services;
using RestfulCatalog.Application.Services;
using RestfulCatalog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddInfrastructure(
                this IServiceCollection services)
        {
            services.AddScoped<
                ICategoryRepository,
                CategoryRepository>();

            services.AddScoped<
                IProductRepository,
                ProductRepository>();

            services.AddScoped<
                ICategoryService,
                CategoryService>();

            services.AddScoped<
                IProductService,
                ProductService>();

            return services;
        }
    }
}
