using System;

namespace AbstractAir.Persistence.Domain
{
	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		private readonly ISessionContextStrategy _sessionContextStrategy;

		public Repository(ISessionContextStrategy sessionContextStrategy)
		{
			_sessionContextStrategy = ArgumentValidation.IsNotNull(sessionContextStrategy, "sessionContextStrategy");
		}

		public TEntity Get(object identifier)
		{
			return _sessionContextStrategy.Retrieve().Get<TEntity>(identifier);
		}
	}
}