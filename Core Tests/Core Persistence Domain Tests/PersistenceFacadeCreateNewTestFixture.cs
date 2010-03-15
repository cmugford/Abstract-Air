using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceFacadeCreateNewTestFixture : PersistenceFacadeTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void ShouldRegisterCreatedEntityWithRepository()
		{
			var creationStrategy = MockRepository.GenerateStub<ICreationStrategy<ITestObject>>();
			var testObject = MockRepository.GenerateStub<ITestObject>();

			creationStrategy.Stub(f => f.CreateNew()).Return(testObject);

			Container.Stub(container => container.GetInstance<ICreationStrategy<ITestObject>>())
				.Return(creationStrategy);

			var result = PersistenceFacade.CreateNew<ITestObject>();

			Assert.AreSame(testObject, result);
		}
	}
}