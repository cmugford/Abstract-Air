using System;

using Norm;

namespace AbstractAir.Examples.QuerySchemas
{
	public class ProductSummary
	{
		[MongoIdentifier]
		public Guid ProductId { get; set; }

		public string Name { get; set; }

		public override int GetHashCode()
		{
			return ProductId.GetHashCode();
		}
	}
}