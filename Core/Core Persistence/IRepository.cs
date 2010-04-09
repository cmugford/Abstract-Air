using System;

namespace AbstractAir.Persistence
{
	public interface IRepository<TEntity>
		where TEntity : class
	{
		TEntity Get(object identifier);
	}
}