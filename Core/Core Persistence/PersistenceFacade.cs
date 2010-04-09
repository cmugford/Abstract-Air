using System;

using StructureMap;

namespace AbstractAir.Persistence
{
	public class PersistenceFacade : IPersistenceFacade
	{
		private readonly IContainer _container;

		[CLSCompliant(false)]
		public PersistenceFacade(IContainer container)
		{
			_container = ArgumentValidation.IsNotNull(container, "container");
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

			_container.GetInstance<ISavingStrategy<TEntity>>().Save(instance);
		}
	}
}