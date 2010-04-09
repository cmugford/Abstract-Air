using System;

namespace AbstractAir.Persistence
{
	public class Repository<TEntity, TBaseEntity> : IRepository<TEntity>
		where TEntity : class
		where TBaseEntity : class, IEntity, TEntity
	{
		private readonly ISessionContextStrategy _sessionContextStrategy;

		public Repository(ISessionContextStrategy sessionContextStrategy)
		{
			_sessionContextStrategy = ArgumentValidation.IsNotNull(sessionContextStrategy, "sessionContextStrategy");
		}

		public TEntity Get(object identifier)
		{
			return _sessionContextStrategy.Retrieve().Get<TBaseEntity>(identifier);
		}
	}
}