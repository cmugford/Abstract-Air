using System;

namespace AbstractAir.Persistence.Domain
{
	public interface IRepository<TEntity>
		where TEntity : class
	{
		TEntity Get(object identifier);
	}
}