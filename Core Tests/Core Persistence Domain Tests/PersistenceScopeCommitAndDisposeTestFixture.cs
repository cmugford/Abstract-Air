using System;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceScopeCommitAndDisposeTestFixture : PersistenceScopeTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void CommitOfScopeCommitsTransactionCreatedFromSession()
		{
			PersistenceScope.Commit();

			Transaction.AssertWasCalled(transaction => transaction.Commit());
		}

		[Test]
		public void SessionDisposedOnScopeDispose()
		{
			PersistenceScope.Dispose();

			Session.AssertWasCalled(session => session.Dispose());
		}

		[Test]
		public void TransactionDisposedOnScopeDispose()
		{
			PersistenceScope.Dispose();

			Transaction.AssertWasCalled(transaction => transaction.Dispose());
		}

		[Test]
		public void TransactionRolledBackOnDisposeIfScopeNotCommitted()
		{
			PersistenceScope.Dispose();

			Transaction.AssertWasCalled(transaction => transaction.Rollback());
		}

		[Test]
		public void TransactionNotRolledBackOnDisposeIfScopeCommitted()
		{
			PersistenceScope.Commit();

			PersistenceScope.Dispose();

			Transaction.AssertWasNotCalled(transaction => transaction.Rollback());
		}

		[Test]
		public void ScopeMayBeDisposedMultipleTimes()
		{
			PersistenceScope.Dispose();
			PersistenceScope.Dispose();
		}
	}
}