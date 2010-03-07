using System;

using NServiceBus;

namespace AbstractAir.Examples.InternalMessages
{
	public interface ICreateProductMessage : IMessage
	{
		string Name { get; set; }
		string Category { get; set; }
	}
}