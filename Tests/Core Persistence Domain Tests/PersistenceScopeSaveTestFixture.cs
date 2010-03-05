using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceScopeSaveTestFixture : PersistenceScopeTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void SaveUsesSavingStrategyFromContext()
		{
			var savingStrategy = MockRepository.GenerateStub<ISavingStrategy<TestObject>>();
			var instance = new TestObject();

			Container.Stub(container => container.GetInstance<ISavingStrategy<TestObject>>()).Return(savingStrategy);

			PersistenceScope.Save(instance);

			savingStrategy.AssertWasCalled(strategy => strategy.Save(instance, Session));
		}
	}
}