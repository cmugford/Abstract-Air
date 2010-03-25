using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class DefaultSavingStrategyTestFixture
	{
		[Test]
		public void SavedInstanceShouldBePersistedInstance()
		{
			var sessionContextStrategy = MockRepository.GenerateStub<ISessionContextStrategy>();
			var session = MockRepository.GenerateStub<ISession>();
			sessionContextStrategy.Stub(strategy => strategy.Retrieve()).Return(session);

			var instance = new TestObject();

			new DefaultSavingStrategy<ITestObject>(sessionContextStrategy).Save(instance);

			session.AssertWasCalled(s => s.Save(instance));
		}
	}
}