using System;

using AbstractAir.TestDomainObjects;
using AbstractAir.TestUtilities;

using MbUnit.Framework;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Persistence.Domain.Tests
{
	[Row(typeof(ICreationStrategy<ITestObject>), typeof(DefaultCreationStrategy<ITestObject, TestObject>))]
	[Row(typeof(ICreationStrategy<IVersionedTestObject>), typeof(DefaultCreationStrategy<IVersionedTestObject, VersionedTestObject>))]
	public class PreExistingCreationStrategyRegistrationTestFixture<TRequestedClass, TNotExpectedClass>
		: InMemoryDatabaseTestFixtureBase
		where TRequestedClass : class
	{
		public PreExistingCreationStrategyRegistrationTestFixture()
			: base(new[] {typeof(TestObject).Assembly})
		{
		}

		public override void SetupImplementation()
		{
		}

		public override void TearDownImplementation()
		{
		}

		[Test]
		public void ValidateRegistrationOfTypeWithPreExistingRegistration()
		{
			ObjectFactory.Configure(configure => configure.For<TRequestedClass>().Use(MockRepository.GenerateStub<TRequestedClass>()));

			var creationStrategyRegistrar = new CreationStrategyRegistrar(SessionFactory);

			creationStrategyRegistrar.Register();

			Assert.IsNotInstanceOfType<TNotExpectedClass>(ObjectFactory.GetInstance<TRequestedClass>());
		}
	}
}