using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	[TestFixture]
	public class DefaultFetchingStrategyTestFixture
	{
		[Test]
		public static void ShouldInvokeRepositoryOnFetch()
		{
			var testObjectRepository = MockRepository.GenerateStub<IRepository<ITestObject>>();
			var testObject = new TestObject();

			testObjectRepository.Stub(repository => repository.Get("TestId")).Return(testObject);

			var result = new DefaultFetchingStrategy<ITestObject>(testObjectRepository).Fetch("TestId");

			Assert.AreSame(testObject, result);
		}
	}
}