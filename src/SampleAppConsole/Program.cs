using System;
using Raven.Client.Document;
using SampleAppConsole.Msmq;

namespace SampleAppConsole
{
    static class Extension
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsNullOrBlank(this string value)
        {
            if (value == null) return true;
            return string.IsNullOrEmpty(value.Trim());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           // Sample_01_Msmq();

           // Sample_02_RavenDB();


            string s = "something";

            Console.WriteLine(s.IsNullOrEmpty());

            s = "";

            Console.WriteLine(s.IsNullOrEmpty());

            s = null;

            s = "";

            Console.WriteLine(s.IsNullOrEmpty());

            Console.WriteLine(s.IsNullOrEmpty());

            Console.WriteLine("hit any key to exit ....");
            Console.ReadKey();
        }



        private static void Sample_02_RavenDB()
        {
            var store = new DocumentStore()
                        {
                            Url = "http://localhost:8080"
                        };

            store.Initialize();
            //store.JsonRequestFactory.DisableRequestCompression = true;

            var orderId = Guid.NewGuid();

            using (var session = store.OpenSession())
            {
                session.Store(new Order()
                              {
                                  OrderId = orderId
                              });
                session.SaveChanges();
            }

            Console.WriteLine("order save: " + orderId);
        }

        private static void Sample_01_Msmq()
        {
            var msmqTest = new MsmqTester();
            msmqTest.SendMessage(
                new MsmqMessage()
                {
                    Amount = 100,
                    DueDate = "2014/11/12 11:30:22",
                    Payee = "Payee1",
                    Payor = "Payer1"
                });

            MsmqMessage msg = msmqTest.ReceiveMessage();

            Console.WriteLine(msg.Payor);
        }
    }

    internal class Order 
    {
        public Guid OrderId { get; set; }
    }
}
