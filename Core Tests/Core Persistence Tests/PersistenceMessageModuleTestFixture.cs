using System;
using System.Transactions;

using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Tests
{
	public class PersistenceMessageModuleTestFixture : PersistenceMessageModuleTestFixtureBase
	{
		private ISession _session;
		private TransactionScope _transactionScope;

		[SetUp]
		public override void Setup()
		{
			base.Setup();

			_session = MockRepository.GenerateStub<ISession>();

			_transactionScope = new TransactionScope();

			SessionFactory.Stub(factory => factory.OpenSession()).Return(_session);
			SessionContextStrategy.Stub(strategy => strategy.Retrieve()).Return(_session);
		}

		[TearDown]
		public void TearDown()
		{
			_transactionScope.Dispose();
			_transactionScope = null;
		}

		[Test]
		public void SessionCreatedOnBeginMessage()
		{
			PersistenceMessageModule.HandleBeginMessage();

			SessionContextStrategy.AssertWasCalled(strategy => strategy.Store(_session));
		}

		[Test]
		public void SessionFlushedOnEndMessage()
		{
			PersistenceMessageModule.HandleEndMessage();

			_session.AssertWasCalled(session => session.Flush());
		}

		[Test]
		public void SessionClosedOnErrorMessage()
		{
			_session.Stub(session => session.IsOpen).Return(true);

			PersistenceMessageModule.HandleError();

			_session.AssertWasCalled(session => session.Close());
		}

		[Test]
		public void SessionRemovedFromContextOnErrorMessage()
		{
			PersistenceMessageModule.HandleError();

			SessionContextStrategy.AssertWasCalled(context => context.Clear());
		}

		[Test]
		public void SessionNotClosedOnErrorMessageIfNotOpen()
		{
			_session.Stub(session => session.IsOpen).Return(false);

			PersistenceMessageModule.HandleError();

			_session.AssertWasNotCalled(session => session.Close());
		}

		[Test]
		public void OpenSessionClosedOnTransactionComplete()
		{
			_session.Stub(session => session.IsOpen).Return(true);
			PersistenceMessageModule.HandleBeginMessage();
			_transactionScope.Dispose();

			_session.AssertWasCalled(session => session.Close());
		}

		[Test]
		public void ClosedSessionNotClosedOnTransactionComplete()
		{
			_session.Stub(session => session.IsOpen).Return(false);
			PersistenceMessageModule.HandleBeginMessage();
			_transactionScope.Dispose();

			_session.AssertWasNotCalled(session => session.Close());
		}

		[Test]
		public void SessionRemovedFromContextOnTransactionClose()
		{
			PersistenceMessageModule.HandleBeginMessage();
			_transactionScope.Dispose();

			SessionContextStrategy.Stub(strategy => strategy.Clear());
		}

		[Test]
		public void TransactionCompletedUnhookedOnError()
		{
			PersistenceMessageModule.HandleBeginMessage();
			PersistenceMessageModule.HandleError();

			_transactionScope.Dispose();

			_session.AssertWasNotCalled(session => session.Close());
		}
	}
}