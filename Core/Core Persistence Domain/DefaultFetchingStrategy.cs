using System;

namespace AbstractAir.Persistence.Domain
{
	public class DefaultFetchingStrategy<TEntity> : IFetchingStrategy<TEntity>
		where TEntity : class, IEntity
	{
		private readonly IRepository<TEntity> _baseRepository;

		public DefaultFetchingStrategy(IRepository<TEntity> baseRepository)
		{
			_baseRepository = ArgumentValidation.IsNotNull(baseRepository, "baseRepository");
		}

		public TEntity Fetch(object entityId)
		{
			return _baseRepository.Get(entityId);
		}
	}
}