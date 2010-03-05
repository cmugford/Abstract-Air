using System;

using AbstractAir.TestDomainObjects;

using MbUnit.Framework;

using NHibernate.Criterion;
using NHibernate.Engine;
using NHibernate.Impl;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceScopeCriteriaCreationTestFixture : PersistenceScopeTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void CriteriaCreatedDetachedCrtieria()
		{
			var sessionImplementor = MockRepository.GenerateStub<ISessionImplementor>();
			var detachedCriteria = DetachedCriteria.For<TestObject>();

			Session.Stub(session => session.GetSessionImplementation()).Return(sessionImplementor);

			var result = (CriteriaImpl) PersistenceScope.GetExecutableCriteria(detachedCriteria);

			Assert.AreSame(sessionImplementor, result.Session);
		}
	}
}