using System;

using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class ThreadStaticSessionContextStrategyTestFixture
	{
		private ThreadStaticSessionContextStrategy _threadStaticSessionContextStrategy;
		private ISession _session;

		[SetUp]
		public void Setup()
		{
			_session = MockRepository.GenerateMock<ISession>();
			_threadStaticSessionContextStrategy = new ThreadStaticSessionContextStrategy();
		}

		[Test]
		public void StoredSessionMayBeRetrieved()
		{
			_threadStaticSessionContextStrategy.Store(_session);

			Assert.AreSame(_session, _threadStaticSessionContextStrategy.Retrieve());
		}

		[Test]
		public void ClearRemovesSessionFromThreadLocal()
		{
			_threadStaticSessionContextStrategy.Store(_session);

			_threadStaticSessionContextStrategy.Clear();

			Assert.IsNull(_threadStaticSessionContextStrategy.Retrieve());
		}
	}
}