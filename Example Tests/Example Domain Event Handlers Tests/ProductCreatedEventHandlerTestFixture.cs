using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.Messages;

using MbUnit.Framework;

using NServiceBus;

using Rhino.Mocks;

namespace AbstractAir.Examples.DomainEventHandlers.Tests
{
	public class ProductCreatedEventHandlerTestFixture
	{
		private const string TestName = "Product Name";
		private const string TestCategory = "Product Category";

		private IBus _bus;
		private ProductCreatedEventHandler _productCreatedEventHandler;
		private Guid _productId;

		[SetUp]
		public void Setup()
		{
			_bus = MockRepository.GenerateStub<IBus>();
			_productCreatedEventHandler = new ProductCreatedEventHandler(_bus);

			_productId = Guid.NewGuid();
		}

		[Test]
		public void ProductCreatedMessagePublishedOnProductCreatedEvent()
		{
			var productCreatedEvent = new ProductCreatedEvent { Product = new Product() };
			productCreatedEvent.Product.Initialise(_productId, TestName, TestCategory);

			_productCreatedEventHandler.Handle(productCreatedEvent);

			_bus.AssertWasCalled(bus => bus.Publish(Arg<Action<IProductCreatedMessage>>.Is.NotNull));
		}
	}
}