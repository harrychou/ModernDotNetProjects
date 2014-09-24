using System;
using SampleAppConsole.Msmq;

namespace SampleAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample_01_Msmq();

            Console.WriteLine("hit any key to exit ....");
            Console.ReadKey();
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
}
