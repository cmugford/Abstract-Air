using System;

using NHibernate;

namespace AbstractAir.Persistence.Domain
{
	public class DefaultSavingStrategy<TEntity> : ISavingStrategy<TEntity>
		where TEntity : class, IEntity
	{
		public void Save(TEntity instance, ISession session)
		{
			ArgumentValidation.IsNotNull(instance, "instance");
			ArgumentValidation.IsNotNull(session, "session");

			session.Save(instance);
		}
	}
}