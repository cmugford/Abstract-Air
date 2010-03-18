using System;

using AbstractAir.Persistence.Domain;

namespace AbstractAir.Examples.Domain
{
	public class ProductCreatedEvent : IDomainEvent
	{
		public Product Product { get; set; }
	}
}