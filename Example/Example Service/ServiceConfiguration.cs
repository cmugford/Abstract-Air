using System;
using System.Configuration;

using AbstractAir.Examples.Domain;
using AbstractAir.Persistence;
using AbstractAir.Persistence.Domain;

using NHibernate.Dialect;
using NHibernate.Driver;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Examples.ExampleService
{
	public class ServiceConfiguration : IWantToRunAtStartup
	{
		public void Run()
		{
			ObjectFactory.Configure(configure =>
			{
				configure.AddRegistry<CoreRegistry>();
				configure.AddRegistry<PersistenceDomainRegistry>();

				configure.For<IPersistenceConfigurator>().Use<PersistenceConfigurator<MsSql2008Dialect, SqlClientDriver>>();
				configure.For<IPersistenceConfiguration>().Use((IPersistenceConfiguration)ConfigurationManager.GetSection("persistenceConfiguration"));
			});

			ObjectFactory.GetInstance<IPersistenceConfigurator>().ConfigurePersistence(new[] { typeof(Product).Assembly });
			ObjectFactory.GetInstance<ICreationStrategyRegistrar>().Register();
		}

		public void Stop()
		{
		}
	}
}