using System;
using NServiceBus;
using Ordering.Messages;

namespace Ordering.Subscriber
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderPlaced message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for Order Id: {0}", message.OrderId);
        }
    }
}
