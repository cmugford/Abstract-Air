using System;
using System.Web.Mvc;

using AbstractAir.Commands;
using AbstractAir.Example.Validators;

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
				scan.AssemblyContainingType<CreateProductMessageValidator>();
				scan.AddAllTypesOf<IController>().NameBy(type => Constants.AreaName + ":" + type.Name.Replace("Controller", ""));
				scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
			});
		}
	}
}
