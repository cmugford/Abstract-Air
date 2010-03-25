using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Persistence.Domain;

using NServiceBus;

namespace AbstractAir.Examples.ExampleService
{
	public class CreateProductMessageHandler : IHandleMessages<ICreateProductMessage>
	{
		private readonly IPersistenceFacade _persistenceFacade;

		public CreateProductMessageHandler(IPersistenceFacade persistenceFacade)
		{
			_persistenceFacade = ArgumentValidation.IsNotNull(persistenceFacade, "persistenceFacade");
		}

		public void Handle(ICreateProductMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			var createProduct = _persistenceFacade.CreateNew<ICreateProduct>();

			createProduct.Initialise(message.ProductId, message.Name, message.Category);

			_persistenceFacade.Save(createProduct);
		}
	}
}