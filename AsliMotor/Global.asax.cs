using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NServiceBus;
using System.Globalization;
using AsliMotor.Helper;

namespace AsliMotor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        IBus bus;
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute { View = "ErrorPages/Oops" });
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        public void RegisterNServiceBus()
        {
            if (bus == null)
            {
                NServiceBus.SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);
                bus = Configure.WithWeb()
                    .Log4Net()
                    .DefaultBuilder()
                    .BinarySerializer()
                    .MsmqTransport()
                        .IsTransactional(true)
                        .PurgeOnStartup(false)
                    .MsmqSubscriptionStorage()
                    .UnicastBus()
                        .LoadMessageHandlers()
                        .ImpersonateSender(true)
                    .CreateBus()
                    .Start(() => Configure.Instance
                        .ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install()
                    );
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterNServiceBus();

            RoleConfig.Configure();

            System.Globalization.CultureInfo nNewCultur = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            nNewCultur.DateTimeFormat.ShortTimePattern = "dd-MM-yyyy";
            nNewCultur.DateTimeFormat.LongDatePattern = "dd, MMMM yyyy";
            nNewCultur.DateTimeFormat.DateSeparator = "-";
            System.Threading.Thread.CurrentThread.CurrentCulture = nNewCultur;
        }
    }
}