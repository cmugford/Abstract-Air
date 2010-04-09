using System;

using AbstractAir.Persistence;

namespace AbstractAir.Examples.Domain
{
	public interface IRenameProduct : IEntity
	{
		void Rename(string productName);
	}
}