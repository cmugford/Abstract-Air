using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

namespace AbstractAir.Persistence.Tests
{
	public class DefaultCreationStrategyTestFixture
	{
		[Test]
		public void ShouldCreateInstance()
		{
			var result = new DefaultCreationStrategy<ITestObject, TestObject>().CreateNew();

			Assert.IsNotNull(result);
			Assert.IsInstanceOfType<TestObject>(result);
		}
	}
}