using System;

using NHibernate;

namespace AbstractAir.Persistence.Domain
{
	public interface ISavingStrategy<TEntity>
		where TEntity : class, IEntity
	{
		void Save(TEntity instance, ISession session);
	}
}