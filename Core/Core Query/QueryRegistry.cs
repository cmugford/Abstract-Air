using System;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Queries
{
	[CLSCompliant(false)]
	public class QueryRegistry : Registry
	{
		public QueryRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.ExcludeType<IQueryConfiguration>();
					scan.WithDefaultConventions();
				});
		}
	}
}