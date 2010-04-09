using System;

namespace AbstractAir.Persistence
{
	public class DefaultCreationStrategy<TEntity, TBaseEntity> : ICreationStrategy<TEntity>
		where TEntity : class, IEntity
		where TBaseEntity : class, IEntity, TEntity, new()
	{
		public TEntity CreateNew()
		{
			return new TBaseEntity();
		}
	}
}