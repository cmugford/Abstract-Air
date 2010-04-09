﻿using System;

using AbstractAir.Persistence;

namespace AbstractAir.Examples.Domain
{
	public class ProductCreatedEvent : IDomainEvent
	{
		public Product Product { get; set; }
	}
}