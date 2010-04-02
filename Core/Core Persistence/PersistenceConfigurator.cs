using System;
using System.Collections.Generic;
using System.Reflection;

using NHibernate;
using NHibernate.ByteCode.LinFu;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

using StructureMap;
using StructureMap.Pipeline;

namespace AbstractAir.Persistence
{
	public class PersistenceConfigurator<TDialect, TDriver> : IPersistenceConfigurator
		where TDialect : Dialect
		where TDriver : DriverBase
	{
		private readonly IPersistenceConfiguration _persistenceConfiguration;

		public PersistenceConfigurator(IPersistenceConfiguration persistenceConfiguration)
		{
			_persistenceConfiguration = ArgumentValidation.IsNotNull(persistenceConfiguration, "persistenceConfiguration");
		}

		public Configuration Configuration { get; private set; }

		public void ConfigurePersistence(IEnumerable<Assembly> assemblies)
		{
			Configuration = new Configuration()
				.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "on_close")
				.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(TDialect).AssemblyQualifiedName)
				.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(TDriver).AssemblyQualifiedName)
				.SetProperty(NHibernate.Cfg.Environment.ConnectionString, _persistenceConfiguration.ConnectionString)
				.SetProperty(NHibernate.Cfg.Environment.ProxyFactoryFactoryClass, typeof(ProxyFactoryFactory).AssemblyQualifiedName)
				.SetProperty(NHibernate.Cfg.Environment.ShowSql, "false");

			assemblies.Apply(assembly => Configuration.AddAssembly(assembly));

			ObjectFactory.Configure(configure
				=> configure.For<ISessionFactory>().LifecycleIs(new SingletonLifecycle()).Use(Configuration.BuildSessionFactory()));
		}
	}
}