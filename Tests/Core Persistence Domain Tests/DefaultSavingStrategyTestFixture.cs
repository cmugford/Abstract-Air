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
			var session = MockRepository.GenerateStub<ISession>();

			var instance = new TestObject();

			new DefaultSavingStrategy<ITestObject>().Save(instance, session);

			session.AssertWasCalled(s => s.Save(instance));
		}
	}
}