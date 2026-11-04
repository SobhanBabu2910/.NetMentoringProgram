using Asp.Versioning;

namespace RestfulCatalog.API
{
    public static class ServiceExtensions
    {
        public static IServiceCollection
            AddApiVersioningConfiguration(
                this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion =
                    new ApiVersion(1, 0);

                options.AssumeDefaultVersionWhenUnspecified =
                    true;

                options.ReportApiVersions = true;
            });

            return services;
        }
    }
}
