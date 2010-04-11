using System;
using System.Linq;
using System.Web.Mvc;

using AbstractAir.Commands;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Portal
{
	[CLSCompliant(false)]
	public class CommandBus : ICommandBus
	{
		private readonly IBus _bus;
		private readonly IContainer _container;

		public CommandBus(IBus bus, IContainer container)
		{
			_bus = ArgumentValidation.IsNotNull(bus, "bus");
			_container = ArgumentValidation.IsNotNull(container, "container");
		}

		public bool Send<TCommand>(Controller controller, Action<TCommand> commandConstructor)
			where TCommand : IMessage
		{
			ArgumentValidation.IsNotNull(controller, "controller");
			ArgumentValidation.IsNotNull(commandConstructor, "command");

			var command = _bus.CreateInstance<TCommand>();

			commandConstructor(command);

			var errors = _container.GetAllInstances<IValidator<TCommand>>()
				.SelectMany(validator => validator.Validate(command))
				.ToList();

			if (errors.Count != 0)
			{
				errors.Apply(error => controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage));
				return false;
			}

			_bus.Send(command);

			return true;
		}
	}
}