using System;
using NServiceBus;

namespace Ordering.Messages
{
    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
    }
}