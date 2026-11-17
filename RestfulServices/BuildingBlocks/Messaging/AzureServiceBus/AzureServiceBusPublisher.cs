using BuildingBlocks.Messaging.Interfaces;
using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace BuildingBlocks.Messaging.AzureServiceBus
{
    public class AzureServiceBusPublisher : IMessagePublisher
    {
        private readonly ServiceBusClient _client;
        private const string TopicName ="product-updated-topic";
        public AzureServiceBusPublisher(ServiceBusClient client)
        {
            _client = client;
        }

        public async Task PublishAsync<T>(T message,CancellationToken cancellationToken = default)
        {
            var sender = _client.CreateSender(TopicName);

            var json = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(json)
            {
                 ContentType = "application/json",
                 MessageId = Guid.NewGuid().ToString()
            };
            await sender.SendMessageAsync(serviceBusMessage,cancellationToken);
        }
    }
}
