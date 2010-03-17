using System;

using AbstractAir.Persistence.Domain;

namespace AbstractAir.Examples.Domain
{
	public interface IRenameProduct : IEntity
	{
		void Rename(string productName);
	}
}