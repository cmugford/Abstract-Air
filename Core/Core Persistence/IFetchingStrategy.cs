using System;

namespace AbstractAir.Persistence
{
	public interface IFetchingStrategy<TEntity>
		where TEntity : class, IEntity
	{
		TEntity Fetch(object entityId);
	}
}