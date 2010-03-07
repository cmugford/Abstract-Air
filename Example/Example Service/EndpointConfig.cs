using System;
using System.Configuration;

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
		public EndpointConfig()
		{
			ObjectFactory.Configure(configure =>
				{
					configure.AddRegistry<CoreRegistry>();
					configure.AddRegistry<PersistenceDomainRegistry>();

					configure.For<IPersistenceConfigurator>().Use<PersistenceConfigurator<MsSql2008Dialect, SqlClientDriver>>();
					configure.For<IPersistenceConfiguration>().Use((IPersistenceConfiguration)ConfigurationManager.GetSection("persistenceConfiguration"));
				});
		}

		public void Init()
		{
			Configure.With()
				.StructureMapBuilder()
				.XmlSerializer()
					.MsmqTransport()
					.IsTransactional(true)
				.PurgeOnStartup(false)
				.UnicastBus()
					.ImpersonateSender(false)
					.LoadMessageHandlers()
				.CreateBus();
		}
	}
}