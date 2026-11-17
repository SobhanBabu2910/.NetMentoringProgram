using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Messaging.Interfaces
{
    public interface IMessageConsumer<T>
    {
        Task HandleAsync(T message);
    }
}
