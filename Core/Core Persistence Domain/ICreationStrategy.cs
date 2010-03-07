using System;

namespace AbstractAir.Persistence.Domain
{
	public interface ICreationStrategy<TEntity>
		where TEntity : class, IEntity
	{
		TEntity CreateNew();
	}
}