using BuildingBlocks.Messaging.AzureServiceBus;
using BuildingBlocks.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Messaging.ServiceBus;

namespace BuildingBlocks
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureServiceBus");

            services.AddSingleton(_ => new ServiceBusClient(connectionString));

            services.AddSingleton<IMessagePublisher,AzureServiceBusPublisher>();

            return services;
        }
    }
}
