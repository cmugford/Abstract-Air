using System;
using System.Web.Mvc;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Example.UI
{
	public class ExampleUIRegistry : Registry
	{
		public ExampleUIRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.AddAllTypesOf<IController>().NameBy(type => Constants.AreaName + ":" + type.Name.Replace("Controller", ""));
				});
		}
	}
}