using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AbstractAir.Portal;

using log4net.Config;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Example.Web.StandAlone
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
				);

			ObjectFactory.Initialize(initialise =>
				{
					initialise.AddRegistry<CoreRegistry>();
					initialise.AddRegistry<StandaloneRegistry>();
				});

			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(ObjectFactory.Container));
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