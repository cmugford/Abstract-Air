using System;

using AbstractAir.TestDomainObjects;
using AbstractAir.TestUtilities;

using MbUnit.Framework;

using StructureMap;

namespace AbstractAir.Persistence.Domain.Tests
{
	[Row(typeof(DefaultCreationStrategy<TestObject, TestObject>), typeof(ICreationStrategy<TestObject>))]
	[Row(typeof(DefaultCreationStrategy<ITestObject, TestObject>), typeof(ICreationStrategy<ITestObject>))]
	[Row(typeof(DefaultCreationStrategy<VersionedTestObject, VersionedTestObject>), typeof(ICreationStrategy<VersionedTestObject>))]
	[Row(typeof(DefaultCreationStrategy<IVersionedTestObject, VersionedTestObject>), typeof(ICreationStrategy<IVersionedTestObject>))]
	[Row(typeof(DefaultCreationStrategy<IFirstTestInterface, MultiInterfaceTestObject>), typeof(ICreationStrategy<IFirstTestInterface>))]
	[Row(typeof(DefaultCreationStrategy<ISecondTestInterface, MultiInterfaceTestObject>), typeof(ICreationStrategy<ISecondTestInterface>))]
	[Row(typeof(DefaultCreationStrategy<IThirdTestInterface, MultiInterfaceTestObject>), typeof(ICreationStrategy<IThirdTestInterface>))]
	[Row(typeof(DefaultCreationStrategy<IEntityInterfaceForNonEntityTestCase, NonEntityInterfaceTestObject>), typeof(ICreationStrategy<IEntityInterfaceForNonEntityTestCase>))]
	public class CreationStrategyRegistrarTestFixture<TConcreteClass, TRequestedClass>
		: InMemoryDatabaseTestFixtureBase
	{
		public CreationStrategyRegistrarTestFixture()
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
		public void ValidateRegistrationOfType()
		{
			var creationStrategyRegistrar = new CreationStrategyRegistrar(SessionFactory);

			creationStrategyRegistrar.Register();

			Assert.IsInstanceOfType<TConcreteClass>(ObjectFactory.GetInstance<TRequestedClass>());
		}
	}
}