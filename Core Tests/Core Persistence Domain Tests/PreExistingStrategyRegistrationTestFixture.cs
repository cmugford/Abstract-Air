using System;

using AbstractAir.TestDomainObjects;
using AbstractAir.TestUtilities;

using MbUnit.Framework;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Persistence.Domain.Tests
{
	[Row(typeof(IRepository<ITestObject>), typeof(Repository<ITestObject, TestObject>))]
	[Row(typeof(ICreationStrategy<ITestObject>), typeof(DefaultCreationStrategy<ITestObject, TestObject>))]
	[Row(typeof(ICreationStrategy<IVersionedTestObject>), typeof(DefaultCreationStrategy<IVersionedTestObject, VersionedTestObject>))]
	public class PreExistingStrategyRegistrationTestFixture<TRequestedClass, TNotExpectedClass>
		: InMemoryDatabaseTestFixtureBase
		where TRequestedClass : class
	{
		public PreExistingStrategyRegistrationTestFixture()
			: base(new[] {typeof(TestObject).Assembly})
		{
		}

		public override void SetupImplementation()
		{
			ObjectFactory.Initialize(initialise => initialise.For<ISessionContextStrategy>().Use(MockRepository.GenerateStub<ISessionContextStrategy>()));
		}

		public override void TearDownImplementation()
		{
		}

		[Test]
		public void ValidateRegistrationOfTypeWithPreExistingRegistration()
		{
			ObjectFactory.Configure(configure => configure.For<TRequestedClass>().Use(MockRepository.GenerateStub<TRequestedClass>()));

			var creationStrategyRegistrar = new StrategyRegistrar(SessionFactory);

			creationStrategyRegistrar.Register();

			Assert.IsNotInstanceOfType<TNotExpectedClass>(ObjectFactory.GetInstance<TRequestedClass>());
		}
	}
}