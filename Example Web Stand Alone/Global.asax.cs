using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NServiceBus;
using log4net.Config;
using StructureMap;
using MvcContrib.ControllerFactories;
using MvcContrib.StructureMap;

namespace AbstractAir.Example.Web.StandAlone
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            ObjectFactory.Initialize(x => x.AddRegistry(new CoreRegistry()));

            ControllerBuilder.Current.SetControllerFactory(
                new IoCControllerFactory(
                    new StructureMapDependencyResolver()));

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ConfigureNServiceBus();
        }
            
		private static void ConfigureNServiceBus()
		{
			Configure.WithWeb()
				.StructureMapBuilder()
				.XmlSerializer()
				.MsmqTransport()
					.IsTransactional(false)
					.PurgeOnStartup(false)
				.UnicastBus()
					.ImpersonateSender(false)
					.LoadMessageHandlers()
				.CreateBus()
				.Start();

			SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
		
        }
    }
}