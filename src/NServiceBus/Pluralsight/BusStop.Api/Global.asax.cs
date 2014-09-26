using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NServiceBus;

namespace BusStop.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IBus Bus { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bus = Configure.With()
                .Log4Net()
                .DefaultBuilder()
                .XmlSerializer()
                .MsmqTransport()
                .UnicastBus()
                .SendOnly();
        }
    }
}
