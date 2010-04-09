using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Persistence;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Examples.ExampleService.Tests
{
	public class RenameProductMessageHandlerTestFixture
	{
		private const string ProductName = "New Product Name";

		private IPersistenceFacade _persistenceFacade;
		private RenameProductMessageHandler _renameProductMessageHandler;
		private Guid _productId;
		private IRenameProduct _renameProduct;
		private IRenameProductMessage _renameProductMessage;

		[SetUp]
		public void Setup()
		{
			_persistenceFacade = MockRepository.GenerateStub<IPersistenceFacade>();
			_renameProduct = MockRepository.GenerateStub<IRenameProduct>();
			_renameProductMessage = MockRepository.GenerateStub<IRenameProductMessage>();

			_productId = Guid.NewGuid();

			_persistenceFacade.Stub(facade => facade.FindById<IRenameProduct>(_productId)).Return(_renameProduct);
			_renameProductMessage.ProductId = _productId;
			_renameProductMessage.Name = ProductName;

			_renameProductMessageHandler = new RenameProductMessageHandler(_persistenceFacade);
		}

		[Test]
		public void ProductRenamed()
		{
			_renameProductMessageHandler.Handle(_renameProductMessage);

			_renameProduct.AssertWasCalled(rename => rename.Rename(ProductName));
		}
	}
}