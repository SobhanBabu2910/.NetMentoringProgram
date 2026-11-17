using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Messaging.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message,CancellationToken cancellationToken = default);
    }
}
