using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.Messages;
using AbstractAir.Persistence;

using NServiceBus;

namespace AbstractAir.Examples.DomainEventHandlers
{
	public class ProductCreatedEventHandler : IHandleDomainEvents<ProductCreatedEvent>
	{
		private readonly IBus _bus;

		[CLSCompliant(false)]
		public ProductCreatedEventHandler(IBus bus)
		{
			_bus = ArgumentValidation.IsNotNull(bus, "bus");
		}

		public void Handle(ProductCreatedEvent instance)
		{
			ArgumentValidation.IsNotNull(instance, "instance");

			_bus.Publish<IProductCreatedMessage>(message =>
				{
					message.ProductId = instance.Product.Id;
					message.Name = instance.Product.Name;
					message.Category = instance.Product.Category;
				});
		}
	}
}