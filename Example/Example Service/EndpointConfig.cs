using System;
using System.Configuration;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.DomainEventHandlers;
using AbstractAir.Persistence;

using log4net.Config;

using NHibernate.Dialect;
using NHibernate.Driver;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Examples.ExampleService
{
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization, IWantCustomLogging
	{
		public void Init()
		{
			ConfigureStructureMap();

			Configure.With()
				.StructureMapBuilder()
				.XmlSerializer();

			SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
		}

		private static void ConfigureStructureMap()
		{
			ObjectFactory.Configure(configure =>
				{
					configure.AddRegistry<CoreRegistry>();
					configure.AddRegistry<PersistenceDomainRegistry>();
					configure.AddRegistry<EventHandlersRegistry>();

					configure.For<IPersistenceConfigurator>().Use<PersistenceConfigurator<MsSql2008Dialect, SqlClientDriver>>();
					configure.For<IPersistenceConfiguration>().Use((IPersistenceConfiguration) ConfigurationManager.GetSection("persistence"));
				});

			ObjectFactory.GetInstance<IPersistenceConfigurator>().ConfigurePersistence(new[] {typeof(Product).Assembly});
			ObjectFactory.GetInstance<IStrategyRegistrar>().Register();

			DomainEvents.Container = ObjectFactory.Container;
		}
	}
}