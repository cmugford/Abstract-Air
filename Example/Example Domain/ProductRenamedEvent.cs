using System;

using AbstractAir.Persistence;

namespace AbstractAir.Examples.Domain
{
	public class ProductRenamedEvent : IDomainEvent
	{
		public Product Product { get; set; }
	}
}