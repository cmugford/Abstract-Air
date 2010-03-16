using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	[TestFixture]
	public class PersistenceFacadeFindByIdTestFixture : PersistenceFacadeTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void FindByIdUsesFetchingStrategy()
		{
			var fetchingStrategy = MockRepository.GenerateStub<IFetchingStrategy<TestObject>>();
			var testObject = new TestObject();
			var testId = Guid.NewGuid();

			Container.Stub(container => container.GetInstance<IFetchingStrategy<TestObject>>()).Return(fetchingStrategy);
			fetchingStrategy.Stub(strategy => strategy.Fetch(testId)).Return(testObject);

			var result = PersistenceFacade.FindById<TestObject>(testId);

			Assert.AreSame(testObject, result);
		}
	}
}