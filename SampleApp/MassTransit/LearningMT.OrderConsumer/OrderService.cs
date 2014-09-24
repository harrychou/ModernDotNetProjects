using MassTransit;

namespace LearningMT.OrderConsumer
{
    public class OrderService
    {
        IServiceBus _bus;

        public void Start()
        {
            _bus = ServiceBusFactory.New(x =>
            {
                x.UseRabbitMq();
                x.ReceiveFrom("rabbitmq://localhost/learningmt_orderservice");
            });

        }

        public void Stop()
        {
            _bus.Dispose();
        }
    }
}