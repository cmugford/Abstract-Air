using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using AbstractAir.Persistence;

using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

using StructureMap;

namespace AbstractAir.Utilities.DatabaseGenerator
{
	public static class Program
	{
		private static UpdatablePersistenceConfiguration _persistenceConfiguration;

		public static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				ShowUsage();
				return;
			}

			ConfigureStructureMap();

			var domains = ExtractDomainInstances(args[0]);

			if (domains.Count == 0)
			{
				ShowUsage();
				return;
			}

			domains.Apply(GenerateDomainDatabase);

			Console.ReadKey();
		}

		private static void GenerateDomainDatabase(DomainInstance domainInstance)
		{
			_persistenceConfiguration.ConnectionString = domainInstance.ConnectionString;

			var persistenceConfigurator = ObjectFactory.GetInstance<IPersistenceConfigurator>();

			var assemblies = domainInstance.Assemblies
				.Select(assemblyName => Assembly.Load(assemblyName))
				.ToList();

			persistenceConfigurator.ConfigurePersistence(assemblies);

			new SchemaExport(persistenceConfigurator.Configuration).Execute(true, true, false);
		}

		private static IList<DomainInstance> ExtractDomainInstances(string fileName)
		{
			var document = XDocument.Load(fileName);
			if (document.Root == null)
			{
				ShowUsage();
				return new List<DomainInstance>();
			}

			return document.Root.Elements()
				.Where(element => element.Attribute("connectionString") != null)
				.Select(element => new DomainInstance
					{
						ConnectionString = element.Attribute("connectionString").Value,
						Assemblies = element.Elements("assembly").Select(assemblyElement => assemblyElement.Value).ToList()
					})
				.ToList();
		}

		private static void ShowUsage()
		{
			Console.WriteLine("Usage: Database Generator <DomainDefinition>");
		}

		private static void ConfigureStructureMap()
		{
			_persistenceConfiguration = new UpdatablePersistenceConfiguration();

			ObjectFactory.Configure(configure =>
				{
					configure.For<IPersistenceConfigurator>().Use<PersistenceConfigurator<MsSql2008Dialect, SqlClientDriver>>();
					configure.For<IPersistenceConfiguration>().Use(_persistenceConfiguration);
				});
		}
	}
}