using System;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Portal
{
	[CLSCompliant(false)]
	public class PortalRegistry : Registry
	{
		public PortalRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});
		}
	}
}