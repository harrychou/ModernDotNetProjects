using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningMT.OrderConsumer;
using NUnit.Framework;

namespace LearningMT.OrderConsumerTests
{
    [TestFixture]
    public class Starting_an_order_service
    {
        OrderService _orderService;

        [TestFixtureSetUp]
        public void Before()
        {
            var orderService = new OrderService();
            orderService.Start();

            _orderService = orderService;


        }

        [TestFixtureTearDown]
        public void Finally()
        {
            if (_orderService != null)
            {
                _orderService.Stop();
                _orderService = null;
            }
        }

        [Test]
        public void Should_create_the_service_bus()
        {
        }
    }
}
