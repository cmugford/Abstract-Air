using System;

using AbstractAir.Examples.Domain;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Persistence.Domain;

using NServiceBus;

namespace AbstractAir.Examples.ExampleService
{
	public class RenameProductMessageHandler : IHandleMessages<IRenameProductMessage>
	{
		private readonly IPersistenceFacade _persistenceFacade;

		public RenameProductMessageHandler(IPersistenceFacade persistenceFacade)
		{
			_persistenceFacade = ArgumentValidation.IsNotNull(persistenceFacade, "persistenceFacade");
		}

		public void Handle(IRenameProductMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			var renameProduct = _persistenceFacade.FindById<IRenameProduct>(message.ProductId);

			renameProduct.Rename(message.Name);
		}
	}
}