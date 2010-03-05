using System;

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
					scan.ExcludeType<PersistenceScope>();
					scan.WithDefaultConventions();
				});

			For(typeof(ISavingStrategy<>)).Use(typeof(DefaultSavingStrategy<>));
		}
	}
}