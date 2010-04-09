using System;
using System.Web.Mvc;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Web.Portal
{
	[CLSCompliant(false)]
	public class PortalRegistry : Registry
	{
		public PortalRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.AddAllTypesOf<IController>().NameBy(type => type.Name.Replace("Controller", ""));
				});
		}
	}
}