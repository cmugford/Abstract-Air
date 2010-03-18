using System;

using NServiceBus;

namespace AbstractAir.Examples.Messages
{
	public interface IProductRenamedMessage : IMessage
	{
		Guid ProductId { get; set; }
		string Name { get; set; }
	}
}