using Microsoft.OpenApi;

namespace RestfulCatalog.API
{
    public static class ConfigureSwaggerOptions
    {
        public static IServiceCollection
            AddSwaggerDocumentation(
                this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Catalog API V1",
                        Version = "v1",
                        Description =
                            "Catalog Service Web API"
                    });

                options.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Title = "Catalog API V2",
                        Version = "v2"
                    });
            });

            return services;
        }
    }
}
