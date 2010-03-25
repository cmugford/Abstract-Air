using System;
using System.Globalization;

using AbstractAir.Examples.Messages;

using NServiceBus;

namespace AbstractAir.Examples.QueryUpdateService
{
	public class ProductCreatedMessageHandler : IHandleMessages<IProductCreatedMessage>
	{
		public void Handle(IProductCreatedMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			Console.WriteLine(string.Format(CultureInfo.CurrentCulture,
				"Product Created: {0}",
				message.ProductId));
		}
	}
}