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

		private IPersistenceFactory _persistenceFactory;
		private IPersistenceScope _persistenceScope;
		private CreateProductMessageHandler _createProductMessageHandler;
		private ICreateProductMessage _createProductMessage;
		private ICreateProduct _createProduct;

		[SetUp]
		public void Setup()
		{
			_persistenceFactory = MockRepository.GenerateStub<IPersistenceFactory>();
			_persistenceScope = MockRepository.GenerateStub<IPersistenceScope>();
			_createProductMessage = MockRepository.GenerateStub<ICreateProductMessage>();
			_createProduct = MockRepository.GenerateStub<ICreateProduct>();

			_persistenceFactory.Stub(factory => factory.CreateScope()).Return(_persistenceScope);
			_persistenceScope.Stub(scope => scope.CreateNew<ICreateProduct>()).Return(_createProduct);
			_createProductMessage.Name = TestName;
			_createProductMessage.Category = TestCategory;

			_createProductMessageHandler = new CreateProductMessageHandler(_persistenceFactory);
		}

		[Test]
		public void PersistenceScopeCommittedForSuccessfulUpdate()
		{
			_createProductMessageHandler.Handle(_createProductMessage);

			_persistenceScope.AssertWasCalled(scope => scope.Commit());
		}

		[Test]
		public void ProductCodeDetailsAssigned()
		{
			_createProductMessageHandler.Handle(_createProductMessage);

			_createProduct.AssertWasCalled(createProduct => createProduct.AssignCoreDetails(TestName, TestCategory));
		}
	}
}