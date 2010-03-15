using System;

using NServiceBus;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Persistence.Domain
{
	[CLSCompliant(false)]
	public class PersistenceDomainRegistry : Registry
	{
		public PersistenceDomainRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});

			For<ISessionContextStrategy>().Use<CallContextSessionContextStrategy>();
			FillAllPropertiesOfType<IPersistenceFacade>();
			For(typeof(ISavingStrategy<>)).Use(typeof(DefaultSavingStrategy<>));
			For<IMessageModule>().Use<PersistenceMessageModule>();
		}
	}
}