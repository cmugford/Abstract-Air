using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Persistence.Domain;

using NServiceBus;

namespace AbstractAir.Examples.ExampleService
{
	public class CreateProductMessageHandler : IHandleMessages<ICreateProductMessage>
	{
		public IPersistenceFacade PersistenceFacade { get; set; }

		public void Handle(ICreateProductMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			var createProduct = PersistenceFacade.CreateNew<ICreateProduct>();

			createProduct.AssignCoreDetails(message.Name, message.Category);

			PersistenceFacade.Save(createProduct);
		}
	}
}