using MbUnit.Framework;

using NHibernate;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceFactoryTestFixture
	{
		private ISessionFactory _sessionFactory;
		private ISession _session;
		private IContainer _container;

		[SetUp]
		public void Setup()
		{
			_sessionFactory = MockRepository.GenerateStub<ISessionFactory>();
			_session = MockRepository.GenerateStub<ISession>();
			_container = MockRepository.GenerateStub<IContainer>();

			_sessionFactory.Stub(factory => factory.OpenSession()).Return(_session);
		}

		[Test]
		public void ScopeCreatedContainsSessionFromSessionFactory()
		{
			var persistence = new PersistenceFactory(_sessionFactory, _container);

			var persistenceScope = persistence.CreateScope();

			Assert.AreSame(_session, ((PersistenceScope) persistenceScope).Session);
		}

		[Test]
		public void ScopeCreatedContainsContainerFromSessionFactory()
		{
			var persistence = new PersistenceFactory(_sessionFactory, _container);

			var persistenceScope = persistence.CreateScope();

			Assert.AreSame(_container, ((PersistenceScope) persistenceScope).Container);
		}
	}
}