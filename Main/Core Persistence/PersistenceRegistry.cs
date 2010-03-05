using System;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Persistence
{
	[CLSCompliant(false)]
	public class PersistenceRegistry : Registry
	{
		public PersistenceRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});
		}
	}
}