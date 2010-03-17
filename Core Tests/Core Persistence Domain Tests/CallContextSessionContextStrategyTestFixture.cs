using System;
using System.Runtime.Remoting.Messaging;

using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class CallContextSessionContextStrategyTestFixture
	{
		private CallContextSessionContextStrategy _callContextSessionContextStrategy;
		private ISession _session;

		[SetUp]
		public void Setup()
		{
			_session = MockRepository.GenerateMock<ISession>();
			_callContextSessionContextStrategy = new CallContextSessionContextStrategy();
		}

		[TearDown]
		public void TearDown()
		{
			CallContext.FreeNamedDataSlot(CallContextSessionContextStrategy.SessionCallContextKey);
		}

		[Test]
		public void StorePlacesSessionInCallContext()
		{
			_callContextSessionContextStrategy.Store(_session);

			Assert.AreSame(_session, CallContext.GetData(CallContextSessionContextStrategy.SessionCallContextKey));
		}

		[Test]
		public void RetrieveObtainsSessionFromCallContext()
		{
			CallContext.SetData(CallContextSessionContextStrategy.SessionCallContextKey, _session);

			Assert.AreSame(_session, _callContextSessionContextStrategy.Retrieve());
		}

		[Test]
		public void ClearRemovesSessionFromCallContext()
		{
			CallContext.SetData(CallContextSessionContextStrategy.SessionCallContextKey, _session);

			_callContextSessionContextStrategy.Clear();

			Assert.IsNull(CallContext.GetData(CallContextSessionContextStrategy.SessionCallContextKey));
		}
	}
}