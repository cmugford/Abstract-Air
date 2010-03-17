using System;

using NServiceBus;

namespace AbstractAir.Examples.InternalMessages
{
	public interface IRenameProductMessage : IMessage
	{
		Guid ProductId { get; set; }
		string Name { get; set; }
	}
}