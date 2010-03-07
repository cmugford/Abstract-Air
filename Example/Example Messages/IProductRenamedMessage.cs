using System;

namespace AbstractAir.Examples.Messages
{
	public interface IProductRenamedMessage
	{
		Guid ProductId { get; set; }
		string Name { get; set; }
	}
}