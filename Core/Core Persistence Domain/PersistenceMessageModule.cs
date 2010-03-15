using System;

using NHibernate;

using NServiceBus;

namespace AbstractAir.Persistence.Domain
{
	public class PersistenceMessageModule : IMessageModule
	{
		private readonly ISessionContextStrategy _sessionContextStrategy;
		private readonly ISessionFactory _sessionFactory;

		public PersistenceMessageModule(ISessionContextStrategy sessionContextStrategy, ISessionFactory sessionFactory)
		{
			_sessionContextStrategy = ArgumentValidation.IsNotNull(sessionContextStrategy, "sessionContextStrategy");
			_sessionFactory = ArgumentValidation.IsNotNull(sessionFactory, "sessionFactory");
		}

		public void HandleBeginMessage()
		{
			_sessionContextStrategy.Store(_sessionFactory.OpenSession());
		}

		public void HandleEndMessage()
		{
			_sessionContextStrategy.Retrieve().Flush();
		}

		public void HandleError()
		{
			var session = _sessionContextStrategy.Retrieve();

			if (session.IsOpen)
			{
				session.Close();
			}
		}
	}
}