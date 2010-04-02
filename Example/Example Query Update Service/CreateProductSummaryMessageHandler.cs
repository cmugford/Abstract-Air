using System;

using AbstractAir.Examples.Messages;
using AbstractAir.Examples.QuerySchemas;

using Norm;

using NServiceBus;

namespace AbstractAir.Examples.QueryUpdateService
{
	public class CreateProductSummaryMessageHandler : IHandleMessages<IProductCreatedMessage>
	{
		private readonly IMongoDatabase _mongoDatabase;

		public CreateProductSummaryMessageHandler(IMongoDatabase mongoDatabase)
		{
			_mongoDatabase = ArgumentValidation.IsNotNull(mongoDatabase, "mongoDatabase");
		}

		public void Handle(IProductCreatedMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			var productSummary = new ProductSummary
				{
					ProductId = message.ProductId,
					Name = message.Name
				};
			_mongoDatabase.GetCollection<ProductSummary>().Insert(productSummary);
		}
	}
}