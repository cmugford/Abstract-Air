using System;
using System.Linq;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Commands
{
	[CLSCompliant(false)]
	public class ValidatingMessageHandler : IHandleMessages<IMessage>
	{
		private readonly IBus _bus;
		private readonly IContainer _container;

		public ValidatingMessageHandler(IBus bus, IContainer container)
		{
			_bus = ArgumentValidation.IsNotNull(bus, "bus");
			_container = ArgumentValidation.IsNotNull(container, "container");
		}

		public void Handle(IMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			var messageType = message.GetType();

			var validationErrors = messageType.GetInterfaces()
				.Where(messageInterface => typeof(IMessage).IsAssignableFrom(messageInterface))
				.Where(messageInterface => messageInterface != typeof(IMessage))
				.Select(messageInterface => typeof(IValidator<>).MakeGenericType(new[] { messageInterface }))
				.SelectMany(validatorInterface => _container.GetAllInstances(validatorInterface)
					.Cast<IValidator>())
				.SelectMany(validator => validator.Validate(message))
				.ToList();

			if (validationErrors.Count == 0)
			{
				return;
			}

			_bus.DoNotContinueDispatchingCurrentMessageToHandlers();
		}
	}
}