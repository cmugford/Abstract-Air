using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.Messages;
using AbstractAir.Persistence.Domain;

using NServiceBus;

namespace AbstractAir.Examples.DomainEventHandlers
{
	public class ProductRenamedEventHandler : IHandleDomainEvents<ProductRenamedEvent>
	{
		private readonly IBus _bus;

		[CLSCompliant(false)]
		public ProductRenamedEventHandler(IBus bus)
		{
			_bus = ArgumentValidation.IsNotNull(bus, "bus");
		}

		public void Handle(ProductRenamedEvent instance)
		{
			ArgumentValidation.IsNotNull(instance, "instance");

			_bus.Publish<IProductRenamedMessage>(message =>
				{
					message.ProductId = instance.Product.Id;
					message.Name = instance.Product.Name;
				});
		}
	}
}