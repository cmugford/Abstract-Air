using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	[TestFixture]
	public class RepositoryTestFixture
	{
		private IRepository<TestObject> _repository;
		private ISession _session;
		private ISessionContextStrategy _sessionContextStrategy;
		private Guid _testId;
		private TestObject _testObject;

		[SetUp]
		public void Setup()
		{
			_sessionContextStrategy = MockRepository.GenerateStub<ISessionContextStrategy>();
			_session = MockRepository.GenerateStub<ISession>();

			_repository = new Repository<TestObject>(_sessionContextStrategy);
			_testObject = new TestObject();
			_testId = Guid.NewGuid();

			_sessionContextStrategy.Stub(strategy => strategy.Retrieve()).Return(_session);
			_session.Stub(session => session.Get<TestObject>(_testId)).Return(_testObject);
		}

		[Test]
		public void RepositoryGetsInstanceFromSession()
		{
			Assert.AreSame(_testObject, _repository.Get(_testId));
		}
	}
}