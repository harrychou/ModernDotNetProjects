using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Raven.Client.Document;
using SampleAppConsole.AsyncSample;
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
        delegate int MyMethod(string s);

        static void Main(string[] args)
        {
            // Sample_01_Msmq();
            // Sample_02_RavenDB();
            // Sample_03_ExtensionMethod_String_NullEmptyChecking();
            // Sample_04_FuncAndAction();


            Stopwatch sw = Stopwatch.StartNew();

           // AsyncProgramSample.DisplayPrimeCounts_3();
            var awaiter = DisplayPrimeCounts().GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);

                Console.WriteLine("end .... hit any key to exit ....");
            });

            Console.ReadKey();

        }

       static async Task DisplayPrimeCounts()
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine(await GetPrimesCountAsync(i * 1000000 + 2, 1000000) +
                " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
            Console.WriteLine("Done!");
        }

        static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine("UNOBSERVED Exception : " + e.Exception.Message);
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
