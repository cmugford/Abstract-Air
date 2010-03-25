using System;
using System.Configuration;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.DomainEventHandlers;
using AbstractAir.Persistence;
using AbstractAir.Persistence.Domain;

using NHibernate.Dialect;
using NHibernate.Driver;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Examples.ExampleService
{
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
	{
		public void Init()
		{
			ConfigureStructureMap();

			Configure.With()
				.StructureMapBuilder()
				.XmlSerializer()
				.MsmqSubscriptionStorage();
		}

		private static void ConfigureStructureMap()
		{
			ObjectFactory.Configure(configure =>
				{
					configure.AddRegistry<CoreRegistry>();
					configure.AddRegistry<PersistenceDomainRegistry>();
					configure.AddRegistry<EventHandlersRegistry>();

					configure.For<IPersistenceConfigurator>().Use<PersistenceConfigurator<MsSql2008Dialect, SqlClientDriver>>();
					configure.For<IPersistenceConfiguration>().Use((IPersistenceConfiguration)ConfigurationManager.GetSection("persistenceConfiguration"));
				});

			ObjectFactory.GetInstance<IPersistenceConfigurator>().ConfigurePersistence(new[] { typeof(Product).Assembly });
			ObjectFactory.GetInstance<IStrategyRegistrar>().Register();

			DomainEvents.Container = ObjectFactory.Container;
		}
	}
}