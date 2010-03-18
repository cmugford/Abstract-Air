using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.Messages;

using MbUnit.Framework;

using NServiceBus;

using Rhino.Mocks;

namespace AbstractAir.Examples.DomainEventHandlers.Tests
{
	public class ProductRenamedEventHandlerTestFixture
	{
		private const string TestCategory = "Product Category";
		private const string TestName = "Product Name";

		private IBus _bus;
		private ProductRenamedEventHandler _productRenamedEventHandler;
		private Guid _productId;

		[SetUp]
		public void Setup()
		{
			_bus = MockRepository.GenerateStub<IBus>();
			_productRenamedEventHandler = new ProductRenamedEventHandler(_bus);

			_productId = Guid.NewGuid();
		}

		[Test]
		public void ProductCreatedMessagePublishedOnProductCreatedEvent()
		{
			var productRenamedEvent = new ProductRenamedEvent {Product = new Product()};
			productRenamedEvent.Product.Initialise(_productId, TestName, TestCategory);

			_productRenamedEventHandler.Handle(productRenamedEvent);

			_bus.AssertWasCalled(bus => bus.Publish(Arg<Action<IProductRenamedMessage>>.Is.NotNull));
		}
	}
}