using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AbstractAir.Example.UI;
using AbstractAir.Example.Validators;
using AbstractAir.Portal;
using AbstractAir.Queries;

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
					initialise.AddRegistry<SiteRegistry>();
					initialise.AddRegistry<ExampleUIRegistry>();
					initialise.AddRegistry<ValidatorsRegistry>();

					initialise.For<IQueryConfiguration>().Use((IQueryConfiguration)ConfigurationManager.GetSection("queries"));
				});

			ObjectFactory.GetInstance<IQueryConfigurator>().ConfigureQuerying();

			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(ObjectFactory.Container));
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