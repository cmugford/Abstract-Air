using System;

using NHibernate;
using NHibernate.Criterion;

using StructureMap;

namespace AbstractAir.Persistence.Domain
{
	public class PersistenceScope : IPersistenceScope
	{
		private bool _disposed;
		private bool _committed;
		private readonly ISession _session;
		private readonly ITransaction _transaction;
		private readonly IContainer _container;

		[CLSCompliant(false)]
		public PersistenceScope(ISession session, IContainer container)
		{
			_session = ArgumentValidation.IsNotNull(session, "session");
			_container = ArgumentValidation.IsNotNull(container, "container");

			_transaction = Session.BeginTransaction();
		}

		public ISession Session
		{
			get { return _session; }
		}

		[CLSCompliant(false)]
		public IContainer Container
		{
			get { return _container; }
		}

		public ICriteria GetExecutableCriteria(DetachedCriteria detachedCriteria)
		{
			ArgumentValidation.IsNotNull(detachedCriteria, "detachedCriteria");

			return detachedCriteria.GetExecutableCriteria(_session);
		}

		public TEntity CreateNew<TEntity>()
			where TEntity : class, IEntity
		{
			return _container.GetInstance<ICreationStrategy<TEntity>>().CreateNew();
		}

		public void Save<TEntity>(TEntity instance)
			where TEntity : class, IEntity
		{
			ArgumentValidation.IsNotNull(instance, "instance");

			_container.GetInstance<ISavingStrategy<TEntity>>().Save(instance, _session);
		}

		public void Commit()
		{
			_transaction.Commit();
			_committed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing || _disposed)
			{
				return;
			}

			if (!_committed)
			{
				_transaction.Rollback();
			}

			_transaction.Dispose();
			Session.Dispose();

			_disposed = true;
		}
	}
}