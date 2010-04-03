using System;

using AbstractAir.Examples.Messages;
using AbstractAir.Examples.QuerySchemas;

using MbUnit.Framework;

using Norm;
using Norm.Collections;

using Rhino.Mocks;

namespace AbstractAir.Examples.QueryUpdateService.Tests
{
	public class CreateProductSummaryMessageHandlerTestFixture
	{
		private const string TestName = "Test Product";

		private IMongoDatabase _mongoDatabase;
		private IMongoCollection<ProductSummary> _mongoCollection;
		private CreateProductSummaryMessageHandler _createProductSummaryMessageHandler;
		private IProductCreatedMessage _productCreatedMessage;
		private Guid _productId;

		[SetUp]
		public void Setup()
		{
			_productId = Guid.NewGuid();

			_mongoDatabase = MockRepository.GenerateStub<IMongoDatabase>();
			_mongoCollection = MockRepository.GenerateStub<IMongoCollection<ProductSummary>>();
			_productCreatedMessage = MockRepository.GenerateStub<IProductCreatedMessage>();

			_mongoDatabase.Stub(database => database.GetCollection<ProductSummary>()).Return(_mongoCollection);
			_productCreatedMessage.ProductId = _productId;
			_productCreatedMessage.Name = TestName;

			_createProductSummaryMessageHandler = new CreateProductSummaryMessageHandler(_mongoDatabase);
		}

		[Test]
		public void ProductSummaryMessagePersistedToMongoOnCreateProductNotification()
		{
			_createProductSummaryMessageHandler.Handle(_productCreatedMessage);

			_mongoCollection.AssertWasCalled(collection => collection.Insert(Arg<ProductSummary[]>
				.Matches(summary => summary[0].ProductId == _productId && summary[0].Name == TestName)));
		}
	}
}