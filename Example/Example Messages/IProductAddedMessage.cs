using System;

using NServiceBus;

namespace AbstractAir.Examples.Messages
{
	public interface IProductAddedMessage : IMessage
	{
		Guid ProductId { get; set; }
		string Name { get; set; }
		string Category { get; set; }
	}
}