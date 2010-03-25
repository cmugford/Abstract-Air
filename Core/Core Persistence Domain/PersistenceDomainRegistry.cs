using System;

using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace AbstractAir.Persistence.Domain
{
	[CLSCompliant(false)]
	public class PersistenceDomainRegistry : Registry
	{
		public PersistenceDomainRegistry()
		{
			For<ISessionContextStrategy>().LifecycleIs(new SingletonLifecycle()).Use<ThreadStaticSessionContextStrategy>();

			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});

			For(typeof(ISavingStrategy<>)).Use(typeof(DefaultSavingStrategy<>));
			For(typeof(IFetchingStrategy<>)).Use(typeof(DefaultFetchingStrategy<>));
		}
	}
}