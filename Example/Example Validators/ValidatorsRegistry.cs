using System;

using AbstractAir.Commands;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Example.Validators
{
	public class ValidatorsRegistry : Registry
	{
		public ValidatorsRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
				});
		}
	}
}