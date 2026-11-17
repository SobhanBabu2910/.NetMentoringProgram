using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Messaging.Events
{
    public sealed record ProductUpdatedEvent(
     int ProductId,
     string Name,
     decimal Price,
     DateTime UpdatedOn);
}
