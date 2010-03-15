using System;

namespace AbstractAir.Persistence.Domain
{
	public interface IPersistenceFacade
	{
		TEntity CreateNew<TEntity>()
			where TEntity : class, IEntity;

		void Save<TEntity>(TEntity instance)
			where TEntity : class, IEntity;
	}
}