using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace BusStop.Authentication
{
    public class AuthenticationHandler: IHandleMessages<IMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(IMessage message)
        {
            var token = message.GetHeader("access_token");

            if (token != "busstop")
            {
                Console.WriteLine("user not authenticated");
                Bus.DoNotContinueDispatchingCurrentMessageToHandlers();
                return;
            }
            Console.WriteLine("user is authenticated");
        }

    }
}
