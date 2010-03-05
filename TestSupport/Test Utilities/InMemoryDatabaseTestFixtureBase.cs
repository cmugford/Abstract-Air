using System;
using System.Collections.Generic;
using System.Reflection;

using AbstractAir.Persistence.Domain;

using MbUnit.Framework;

using NHibernate;
using NHibernate.ByteCode.LinFu;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.TestUtilities
{
	public abstract class InMemoryDatabaseTestFixtureBase : IDisposable
	{
		protected Configuration Configuration { get; private set; }
		protected ISessionFactory SessionFactory { get; private set; }
		protected ISession Session { get; private set; }
		protected IContainer Container { get; private set; }

		protected InMemoryDatabaseTestFixtureBase(IEnumerable<Assembly> assemblies)
		{
			Configuration = new Configuration()
				.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "on_close")
				.SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
				.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
				.SetProperty(NHibernate.Cfg.Environment.ConnectionString, "data source=:memory:")
				.SetProperty(NHibernate.Cfg.Environment.ProxyFactoryFactoryClass, typeof(ProxyFactoryFactory).AssemblyQualifiedName);

			assemblies.Apply(assembly => Configuration.AddAssembly(assembly));

			SessionFactory = Configuration.BuildSessionFactory();
		}

		public IPersistenceScope CreatePersistenceScope()
		{
			return new PersistenceScope(Session, Container);
		}

		[SetUp]
		public void Setup()
		{
			Session = SessionFactory.OpenSession();
			Container = MockRepository.GenerateStub<IContainer>();

			new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);

			SetupImplementation();
		}

		[TearDown]
		public void TearDown()
		{
			TearDownImplementation();

			Session.Dispose();
			Session = null;
		}

		public abstract void SetupImplementation();
		public abstract void TearDownImplementation();

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			if (Session != null)
			{
				Session.Dispose();
			}
		}
	}
}