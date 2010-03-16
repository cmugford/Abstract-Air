using System;

namespace AbstractAir.Persistence.Domain
{
	public class RepositoryWrapper<TEntity, TBaseEntity> : IRepository<TEntity>
		where TEntity : class, IEntity
		where TBaseEntity : class, IEntity, TEntity
	{
		private readonly IRepository<TBaseEntity> _baseRepository;

		public RepositoryWrapper(IRepository<TBaseEntity> baseRepository)
		{
			_baseRepository = ArgumentValidation.IsNotNull(baseRepository, "baseRepository");
		}

		public TEntity Get(object identifier)
		{
			return _baseRepository.Get(identifier);
		}
	}
}