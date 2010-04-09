using System;
using System.Transactions;

using MbUnit.Framework;

namespace AbstractAir.Persistence.Tests
{
	public class PersistenceMessageModuleWithNoSessionTestFixture : PersistenceMessageModuleTestFixtureBase
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
		public void TransactionCompletedHandlerHandlesClearContext()
		{
			PersistenceMessageModule.HandleBeginMessage();

			_transactionScope.Dispose();
		}

		[Test]
		public void ErrorHandlerSucceedsWithNoSessionInContext()
		{
			PersistenceMessageModule.HandleError();
		}
	}
}