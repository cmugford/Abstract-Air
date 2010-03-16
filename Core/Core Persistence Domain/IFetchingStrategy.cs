using System;

namespace AbstractAir.Persistence.Domain
{
	public interface IFetchingStrategy<TEntity>
		where TEntity : class, IEntity
	{
		TEntity Fetch(object entityId);
	}
}