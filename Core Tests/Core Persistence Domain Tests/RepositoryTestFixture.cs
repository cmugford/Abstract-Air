using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class RepositoryTestFixture
	{
		private ISession _session;
		private ISessionContextStrategy _sessionContextStrategy;
		private Guid _testId;
		private TestObject _testObject;

		[SetUp]
		public void Setup()
		{
			_sessionContextStrategy = MockRepository.GenerateStub<ISessionContextStrategy>();
			_session = MockRepository.GenerateStub<ISession>();

			_testObject = new TestObject();
			_testId = Guid.NewGuid();

			_sessionContextStrategy.Stub(strategy => strategy.Retrieve()).Return(_session);
			_session.Stub(session => session.Get<TestObject>(_testId)).Return(_testObject);
		}

		[Test]
		public void RepositoryGetsInstanceFromSession()
		{
			var repository = new Repository<TestObject, TestObject>(_sessionContextStrategy);

			Assert.AreSame(_testObject, repository.Get(_testId));
		}

		[Test]
		public void RepositoryGetsInstanceAsBaseTypeForEntityInterface()
		{
			var repository = new Repository<ITestObject, TestObject>(_sessionContextStrategy);

			Assert.AreSame(_testObject, repository.Get(_testId));
		}
	}
}