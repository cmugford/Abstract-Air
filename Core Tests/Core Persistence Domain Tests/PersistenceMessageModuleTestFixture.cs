using System;
using System.Transactions;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceMessageModuleTestFixture : PersistenceMessageModuleTestFixtureBase
	{
		private TransactionScope _transactionScope;

		[SetUp]
		public override void Setup()
		{
			base.Setup();

			_transactionScope = new TransactionScope();
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

			SessionContextStrategy.AssertWasCalled(strategy => strategy.Store(Session));
		}

		[Test]
		public void SessionFlushedOnEndMessage()
		{
			PersistenceMessageModule.HandleEndMessage();

			Session.AssertWasCalled(session => session.Flush());
		}

		[Test]
		public void SessionClosedOnErrorMessage()
		{
			Session.Stub(session => session.IsOpen).Return(true);

			PersistenceMessageModule.HandleError();

			Session.AssertWasCalled(session => session.Close());
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
			Session.Stub(session => session.IsOpen).Return(false);

			PersistenceMessageModule.HandleError();

			Session.AssertWasNotCalled(session => session.Close());
		}

		[Test]
		public void OpenSessionClosedOnTransactionComplete()
		{
			Session.Stub(session => session.IsOpen).Return(true);
			PersistenceMessageModule.HandleBeginMessage();
			_transactionScope.Dispose();

			Session.AssertWasCalled(session => session.Close());
		}

		[Test]
		public void ClosedSessionNotClosedOnTransactionComplete()
		{
			Session.Stub(session => session.IsOpen).Return(false);
			PersistenceMessageModule.HandleBeginMessage();
			_transactionScope.Dispose();

			Session.AssertWasNotCalled(session => session.Close());
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

			Session.AssertWasNotCalled(session => session.Close());
		}

		[Test]
		public void TransactionCompletedHandlerHandlesClearContext()
		{
			var sessionContextStrategy = MockRepository.GenerateStub<ISessionContextStrategy>();
			var persistenceMessageModule = new PersistenceMessageModule(sessionContextStrategy, SessionFactory);

			persistenceMessageModule.HandleBeginMessage();

			_transactionScope.Dispose();
		}
	}
}