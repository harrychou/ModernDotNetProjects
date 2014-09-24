using System;
using MassTransit;

namespace LearningMT.OrderConsumer
{
    public interface SubmitOrder :
        CorrelatedBy<Guid>
    {
        DateTime SubmitDate { get; }
        string CustomerNumber { get; }
        string OrderNumber { get; }
    }
}