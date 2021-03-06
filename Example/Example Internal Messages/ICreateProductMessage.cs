﻿using System;

using NServiceBus;

namespace AbstractAir.Examples.InternalMessages
{
	public interface ICreateProductMessage : IMessage
	{
		Guid ProductId { get; set; }
		string Name { get; set; }
		string Category { get; set; }
	}
}