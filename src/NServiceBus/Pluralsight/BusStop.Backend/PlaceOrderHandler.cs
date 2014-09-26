using System;
using BusStop.Contracts;
using NServiceBus;
using NServiceBus.UnitOfWork;
using Raven.Client;
using Raven.Client.Document;

namespace BusStop.Backend
{
    public class RavenBootstrapper : INeedInitialization
    {
        public void Init()
        {
            Configure.Instance.Configurer.ConfigureComponent<IDocumentStore>(
                () => 
                {
                    var store = new DocumentStore()
                    {
                        Url = "http://localhost:8080"
                    };

                    store.Initialize();
                    store.JsonRequestFactory.DisableRequestCompression = true;
                    return store;
                }, DependencyLifecycle.SingleInstance);

            Configure.Instance.Configurer.ConfigureComponent<IDocumentSession>(
                () => Configure.Instance.Builder.Build<IDocumentStore>().OpenSession(),
                DependencyLifecycle.InstancePerUnitOfWork);

            Configure.Instance.Configurer.ConfigureComponent<RavenUnitOfWork>(DependencyLifecycle.InstancePerUnitOfWork);
        }
    }

    public class RavenUnitOfWork : IManageUnitsOfWork
    {
        public IDocumentSession Session { get; set; }

        public void Begin()
        {
            
        }

        public void End(Exception ex = null)
        {
//            if (ex == null) Session.SaveChanges();
//            if (ex == null) Console.WriteLine(" - RavenDb save change called");
        }
    }

    public class PlaceOrderHandler: IHandleMessages<PlaceOrder>
    {
        public IDocumentSession Session{ get; set; }

        public void Handle(PlaceOrder message)
        {
            //throw new Exception("Database is down");

            Session.Store(new Order()
                            {
                                OrderId = message.OrderId
                            });
            
            Console.WriteLine("Order Received: " + message.OrderId);
        }
    }

    public class Order
    {
        public Guid OrderId { get; set; }
    }
}