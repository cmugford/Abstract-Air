using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Persistence.Domain;

using NServiceBus;

namespace AbstractAir.Examples.ExampleService
{
	public class CreateProductMessageHandler : IHandleMessages<ICreateProductMessage>
	{
		private readonly IPersistenceFactory _persistenceFactory;

		public CreateProductMessageHandler(IPersistenceFactory persistenceFactory)
		{
			_persistenceFactory = ArgumentValidation.IsNotNull(persistenceFactory, "persistenceFactory");
		}

		public void Handle(ICreateProductMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			using (var persistenceScope = _persistenceFactory.CreateScope())
			{
				var createProduct = persistenceScope.CreateNew<ICreateProduct>();

				createProduct.AssignCoreDetails(message.Name, message.Category);

				persistenceScope.Commit();
			}
		}
	}
}