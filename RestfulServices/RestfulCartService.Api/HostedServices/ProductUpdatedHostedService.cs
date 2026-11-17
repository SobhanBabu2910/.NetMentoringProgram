using Azure.Messaging.ServiceBus;
using BuildingBlocks.Messaging.Events;
using Microsoft.Extensions.Options;
using RestfulCartService.Application.Consumers;
using System.Text.Json;

namespace RestfulCartService.Api.HostedServices
{
    public sealed class ProductUpdatedHostedService: BackgroundService
    {
        private readonly ServiceBusClient _serviceBusClient;

        private readonly IServiceScopeFactory _scopeFactory;

        private ServiceBusProcessor? _processor;

        public ProductUpdatedHostedService(
            ServiceBusClient serviceBusClient,
            IServiceScopeFactory scopeFactory)
        {
            _serviceBusClient = serviceBusClient;

            _scopeFactory = scopeFactory;

        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _processor =_serviceBusClient.CreateProcessor(
                        "product-updated-topic",
                        "cart-subscription",
                        new ServiceBusProcessorOptions
                        {
                            AutoCompleteMessages = false,
                            MaxConcurrentCalls = 5
                        });

            _processor.ProcessMessageAsync += ProcessMessageAsync;

            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(stoppingToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            try
            {
                var body =args.Message.Body.ToString();

                var message =JsonSerializer.Deserialize<ProductUpdatedEvent>(body);

                using var scope =_scopeFactory.CreateScope();

                var consumer =scope.ServiceProvider.GetRequiredService<ProductUpdatedConsumer>();

                await consumer.HandleAsync(message!);

                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {
                await args.AbandonMessageAsync(args.Message);
            }
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception);

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_processor is not null)
            {
                await _processor.StopProcessingAsync(cancellationToken);

                await _processor.DisposeAsync();
            }

            await base.StopAsync(cancellationToken);
        }
    }
}
