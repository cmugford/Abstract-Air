using System;

using AbstractAir.Persistence.Domain;

namespace AbstractAir.Examples.Domain
{
	public interface ICreateProduct : IVersionedEntity
	{
		void Initialise(Guid productId, string productName, string productCategory);
	}
}