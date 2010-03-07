using System;

using NHibernate;
using NHibernate.Criterion;

namespace AbstractAir.Persistence.Domain
{
	public interface IPersistenceScope : IDisposable
	{
		ICriteria GetExecutableCriteria(DetachedCriteria detachedCriteria);

		TEntity CreateNew<TEntity>()
			where TEntity : class, IEntity;

		void Save<TEntity>(TEntity instance)
			where TEntity : class, IEntity;

		void Commit();
	}
}