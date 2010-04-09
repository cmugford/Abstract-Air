using System;

namespace AbstractAir.Persistence
{
	public interface ISavingStrategy<TEntity>
		where TEntity : class, IEntity
	{
		void Save(TEntity instance);
	}
}