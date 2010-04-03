using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AbstractAir.Queries;

using MvcContrib.ControllerFactories;
using MvcContrib.StructureMap;
using MvcContrib.UI.InputBuilder;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Web.Portal
{
	public class MvcApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("elmah.axd");
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Default",
				"{controller}/{action}/{id}",
				new {controller = "Home", action = "Index", id = UrlParameter.Optional});

			ObjectFactory.Configure(initialise =>
				{
					initialise.AddRegistry<CoreRegistry>();
					initialise.AddRegistry<QueryRegistry>();
					initialise.AddRegistry<PortalRegistry>();

					initialise.For<IQueryConfiguration>().Use((IQueryConfiguration)ConfigurationManager.GetSection("queries"));
				});

			ObjectFactory.GetInstance<IQueryConfigurator>().ConfigureQuerying();

			ControllerBuilder.Current.SetControllerFactory(new IoCControllerFactory(new StructureMapDependencyResolver()));
		}

		protected void Application_Start()
		{
			ConfigureNServiceBus();

			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);

			InputBuilder.BootStrap();
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
		}
	}
}