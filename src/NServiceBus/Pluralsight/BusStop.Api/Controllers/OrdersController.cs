using System;
using System.Web;
using System.Web.Http;
using BusStop.Contracts;
using NServiceBus;

namespace BusStop.Api.Controllers
{
    public class OrdersController : ApiController
    {
        public Guid Get()
        {
            var order = new PlaceOrder()
                        {
                            OrderId = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            CustomerId = Guid.NewGuid()
                        };

            WebApiApplication.Bus.Send(order);

            return order.OrderId;
        }
    }
}
