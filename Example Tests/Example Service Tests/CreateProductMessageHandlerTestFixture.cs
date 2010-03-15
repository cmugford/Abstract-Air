using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Persistence.Domain;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Examples.ExampleService.Tests
{
	[TestFixture]
	public class CreateProductMessageHandlerTestFixture
	{
		private const string TestName = "Product Name";
		private const string TestCategory = "Product Category";

		private IPersistenceFacade _persistenceFacade;
		private CreateProductMessageHandler _createProductMessageHandler;
		private ICreateProductMessage _createProductMessage;
		private ICreateProduct _createProduct;

		[SetUp]
		public void Setup()
		{
			_persistenceFacade = MockRepository.GenerateStub<IPersistenceFacade>();
			_createProductMessage = MockRepository.GenerateStub<ICreateProductMessage>();
			_createProduct = MockRepository.GenerateStub<ICreateProduct>();

			_persistenceFacade.Stub(scope => scope.CreateNew<ICreateProduct>()).Return(_createProduct);
			_createProductMessage.Name = TestName;
			_createProductMessage.Category = TestCategory;

			_createProductMessageHandler = new CreateProductMessageHandler { PersistenceFacade = _persistenceFacade };
		}

		[Test]
		public void SaveCalledOnProductCreated()
		{
			_createProductMessageHandler.Handle(_createProductMessage);

			_persistenceFacade.AssertWasCalled(facade => facade.Save(_createProduct));
		}

		[Test]
		public void ProductCodeDetailsAssigned()
		{
			_createProductMessageHandler.Handle(_createProductMessage);

			_createProduct.AssertWasCalled(createProduct => createProduct.AssignCoreDetails(TestName, TestCategory));
		}
	}
}