using MassTransit;

namespace LearningMT.OrderConsumer
{
    public class SubmitOrderConsumer :
        Consumes<SubmitOrder>.Context
    {
        public void Consume(IConsumeContext<SubmitOrder> context)
        {
        }
    }
}
