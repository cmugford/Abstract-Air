using System;

namespace AbstractAir.Persistence
{
	public interface ICreationStrategy<TEntity>
		where TEntity : class, IEntity
	{
		TEntity CreateNew();
	}
}