using System;

using AbstractAir.Persistence.Domain;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Examples.DomainEventHandlers
{
	[CLSCompliant(false)]
	public class EventHandlersRegistry : Registry
	{
		public EventHandlersRegistry()
		{
			Scan(scanner =>
				{
					scanner.TheCallingAssembly();
					scanner.ConnectImplementationsToTypesClosing(typeof(IHandleDomainEvents<>));
				});
		}
	}
}