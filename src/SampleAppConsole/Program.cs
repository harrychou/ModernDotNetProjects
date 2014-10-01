using System;
using System.Collections.Generic;
using System.Data;
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

    public class StringConverter
    {
        
    }

    class Program
    {
        delegate int MyMethod(string s);

        static void Main(string[] args)
        {
            Console.WriteLine("start .... ");

            var rules = new Dictionary<string, string>();
            rules.Add("AB", "AA");
            rules.Add("BA", "AA");
            rules.Add("CB", "CC");
            rules.Add("BC", "CC");
            rules.Add("AA", "A");
            rules.Add("CC", "C");

            string S = "ABCDE";
            foreach (char c in S)
            {
                Console.WriteLine(c);
            }


            // Sample_01_Msmq();
            // Sample_02_RavenDB();
            // Sample_03_ExtensionMethod_String_NullEmptyChecking();
            // Sample_04_FuncAndAction();

            Console.WriteLine("end .... hit any key to exit ....");
            Console.ReadKey();
        }

        private static void Sample_04_FuncAndAction()
        {
            MyMethod m = RealMethod;
            Console.WriteLine(m("harry"));
            RunThisAction(s => Console.WriteLine(s.Length), "harry");
            Console.WriteLine(RunThisFunc(s => s.Length, "harry"));
        }

        private static void RunThisAction(Action<string> action, string s)
        {
            action(s);
        }

        private static int RunThisFunc(Func<string, int> func, string s)
        {
            return func(s);
        }

        private static int RealMethod(string ss)
        {
            if (ss == null) return 0;
            return ss.Length;
        }

        private static void Sample_03_ExtensionMethod_String_NullEmptyChecking()
        {
            string s = "something";
            Console.WriteLine(s.IsNullOrEmpty());
            s = "";
            Console.WriteLine(s.IsNullOrEmpty());
            s = null;
            Console.WriteLine(s.IsNullOrEmpty());
            s = "  ";
            Console.WriteLine(s.IsNullOrEmpty());
            Console.WriteLine(s.IsNullOrBlank());
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
