using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;
using NServiceBus.MessageMutator;

namespace BusStop.Api.Authentication
{
    public class AuthenticationMutator: IMutateOutgoingTransportMessages, INeedInitialization
    {
        public void MutateOutgoing(object[] messages, TransportMessage transportMessage)
        {
            transportMessage.Headers["access_token"] = HttpContext.Current.Request.Params["access_token"];
        }

        public void Init()
        {
            Configure.Instance.Configurer.ConfigureComponent<AuthenticationMutator>(DependencyLifecycle.InstancePerCall);
        }
    }
}