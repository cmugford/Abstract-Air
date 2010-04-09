using System;

namespace AbstractAir.Persistence
{
	public interface IPersistenceFacade
	{
		TEntity FindById<TEntity>(object entityId)
			where TEntity : class, IEntity;

		TEntity CreateNew<TEntity>()
			where TEntity : class, IEntity;

		void Save<TEntity>(TEntity instance)
			where TEntity : class, IEntity;
	}
}