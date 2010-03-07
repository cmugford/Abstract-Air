using System;

using AbstractAir.Persistence.Domain;

namespace AbstractAir.Examples.Domain
{
	public interface ICreateProduct : IVersionedEntity
	{
		void AssignCoreDetails(string productName, string productCategory);
	}
}