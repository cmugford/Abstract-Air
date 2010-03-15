using System;

using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	[TestFixture]
	public class PersistenceMessageModuleTestFixture
	{
		private ISessionContextStrategy _sessionContextStrategy;
		private ISessionFactory _sessionFactory;
		private ISession _session;
		private PersistenceMessageModule _persistenceMessageModule;

		[SetUp]
		public void Setup()
		{
			_sessionContextStrategy = MockRepository.GenerateStub<ISessionContextStrategy>();
			_sessionFactory = MockRepository.GenerateStub<ISessionFactory>();
			_session = MockRepository.GenerateStub<ISession>();

			_sessionFactory.Stub(factory => factory.OpenSession()).Return(_session);
			_sessionContextStrategy.Stub(strategy => strategy.Retrieve()).Return(_session);

			_persistenceMessageModule = new PersistenceMessageModule(_sessionContextStrategy, _sessionFactory);
		}

		[Test]
		public void SessionCreatedOnBeginMessage()
		{
			_persistenceMessageModule.HandleBeginMessage();

			_sessionContextStrategy.AssertWasCalled(strategy => strategy.Store(_session));
		}

		[Test]
		public void SessionFlushedOnEndMessage()
		{
			_persistenceMessageModule.HandleEndMessage();

			_session.AssertWasCalled(session => session.Flush());
		}

		[Test]
		public void SessionClosedOnErrorMessage()
		{
			_session.Stub(session => session.IsOpen).Return(true);

			_persistenceMessageModule.HandleError();

			_session.AssertWasCalled(session => session.Close());
		}

		[Test]
		public void SessionNotClosedOnErrorMessageIfNotOpen()
		{
			_session.Stub(session => session.IsOpen).Return(false);

			_persistenceMessageModule.HandleError();

			_session.AssertWasNotCalled(session => session.Close());
		}
	}
}