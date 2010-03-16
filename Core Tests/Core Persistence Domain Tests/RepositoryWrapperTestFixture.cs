using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public static class RepositoryWrapperTestFixture
	{
		[Test]
		public static void ShouldInvokeBaseRepositoryOnGet()
		{
			var testObjectRepository = MockRepository.GenerateStub<IRepository<TestObject>>();
			var testObject = new TestObject();

			testObjectRepository.Stub(repository => repository.Get("TestId")).Return(testObject);

			var result = new RepositoryWrapper<ITestObject, TestObject>(testObjectRepository).Get("TestId");

			Assert.AreSame(testObject, result);
		}
	}
}