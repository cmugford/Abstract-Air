﻿using System;

using AbstractAir.TestDomainObjects;
using AbstractAir.TestUtilities;

using MbUnit.Framework;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Persistence.Tests
{
	[Row(typeof(Repository<TestObject, TestObject>), typeof(IRepository<TestObject>))]
	[Row(typeof(Repository<ITestObject, TestObject>), typeof(IRepository<ITestObject>))]
	[Row(typeof(Repository<IFirstTestInterface, MultiInterfaceTestObject>), typeof(IRepository<IFirstTestInterface>))]
	[Row(typeof(Repository<ISecondTestInterface, MultiInterfaceTestObject>), typeof(IRepository<ISecondTestInterface>))]
	[Row(typeof(Repository<IThirdTestInterface, MultiInterfaceTestObject>), typeof(IRepository<IThirdTestInterface>))]
	[Row(typeof(Repository<IEntityInterfaceForNonEntityTestCase, NonEntityInterfaceTestObject>), typeof(IRepository<IEntityInterfaceForNonEntityTestCase>))]
	[Row(typeof(DefaultCreationStrategy<TestObject, TestObject>), typeof(ICreationStrategy<TestObject>))]
	[Row(typeof(DefaultCreationStrategy<ITestObject, TestObject>), typeof(ICreationStrategy<ITestObject>))]
	[Row(typeof(DefaultCreationStrategy<VersionedTestObject, VersionedTestObject>), typeof(ICreationStrategy<VersionedTestObject>))]
	[Row(typeof(DefaultCreationStrategy<IVersionedTestObject, VersionedTestObject>), typeof(ICreationStrategy<IVersionedTestObject>))]
	[Row(typeof(DefaultCreationStrategy<IFirstTestInterface, MultiInterfaceTestObject>), typeof(ICreationStrategy<IFirstTestInterface>))]
	[Row(typeof(DefaultCreationStrategy<ISecondTestInterface, MultiInterfaceTestObject>), typeof(ICreationStrategy<ISecondTestInterface>))]
	[Row(typeof(DefaultCreationStrategy<IThirdTestInterface, MultiInterfaceTestObject>), typeof(ICreationStrategy<IThirdTestInterface>))]
	[Row(typeof(DefaultCreationStrategy<IEntityInterfaceForNonEntityTestCase, NonEntityInterfaceTestObject>), typeof(ICreationStrategy<IEntityInterfaceForNonEntityTestCase>))]
	public class StrategyRegistrarTestFixture<TConcreteClass, TRequestedClass>
		: InMemoryDatabaseTestFixtureBase
	{
		public StrategyRegistrarTestFixture()
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
		public void ValidateRegistrationOfType()
		{
			var creationStrategyRegistrar = new StrategyRegistrar(SessionFactory);

			creationStrategyRegistrar.Register();

			Assert.IsInstanceOfType<TConcreteClass>(ObjectFactory.GetInstance<TRequestedClass>());
		}
	}
}