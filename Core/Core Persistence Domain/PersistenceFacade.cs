using System;

using StructureMap;

namespace AbstractAir.Persistence.Domain
{
	public class PersistenceFacade : IPersistenceFacade
	{
		private readonly IContainer _container;
		private readonly ISessionContextStrategy _sessionContextStrategy;

		[CLSCompliant(false)]
		public PersistenceFacade(IContainer container, ISessionContextStrategy sessionContextStrategy)
		{
			_container = ArgumentValidation.IsNotNull(container, "container");
			_sessionContextStrategy = ArgumentValidation.IsNotNull(sessionContextStrategy, "sessionContextStrategy");
		}

		[CLSCompliant(false)]
		public IContainer Container
		{
			get { return _container; }
		}

		public TEntity FindById<TEntity>(object entityId) where TEntity : class, IEntity
		{
			return _container.GetInstance<IFetchingStrategy<TEntity>>().Fetch(entityId);
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

			_container.GetInstance<ISavingStrategy<TEntity>>().Save(instance, _sessionContextStrategy.Retrieve());
		}
	}
}