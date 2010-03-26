using System;
using System.Transactions;

using AbstractAir.Persistence.Domain.Properties;

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
			var transaction = Transaction.Current;

			if (transaction == null)
			{
				throw new InvalidOperationException(Resources.NoAmbientTransactionError);
			}

			_sessionContextStrategy.Store(_sessionFactory.OpenSession());

			transaction.TransactionCompleted += TransactionCompleted;
		}

		public void HandleEndMessage()
		{
			_sessionContextStrategy.Retrieve().Flush();
		}

		public void HandleError()
		{
			var session = _sessionContextStrategy.Retrieve();

			if (session != null && session.IsOpen)
			{
				session.Close();
			}

			_sessionContextStrategy.Clear();

			Transaction.Current.TransactionCompleted -= TransactionCompleted;
		}

		private void TransactionCompleted(object sender, TransactionEventArgs e)
		{
			var session = _sessionContextStrategy.Retrieve();
			if (session != null && session.IsOpen)
			{
				session.Close();
			}
			_sessionContextStrategy.Clear();
		}
	}
}