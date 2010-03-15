using System;

using NHibernate;

namespace AbstractAir.Persistence.Domain
{
	public interface ISessionContextStrategy
	{
		void Store(ISession session);

		ISession Retrieve();

		void Clear();
	}
}