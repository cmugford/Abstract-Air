using System;

namespace AbstractAir.Persistence
{
	public class DefaultSavingStrategy<TEntity> : ISavingStrategy<TEntity>
		where TEntity : class, IEntity
	{
		private readonly ISessionContextStrategy _sessionContextStrategy;

		public DefaultSavingStrategy(ISessionContextStrategy sessionContextStrategy)
		{
			_sessionContextStrategy = ArgumentValidation.IsNotNull(sessionContextStrategy, "sessionContextStrategy");
		}

		public void Save(TEntity instance)
		{
			ArgumentValidation.IsNotNull(instance, "instance");

			_sessionContextStrategy.Retrieve().Save(instance);
		}
	}
}