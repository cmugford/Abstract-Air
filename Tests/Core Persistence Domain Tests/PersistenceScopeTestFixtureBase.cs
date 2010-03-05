using NHibernate;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceScopeTestFixtureBase
	{
		public ISession Session { get; private set; }
		public PersistenceScope PersistenceScope { get; private set; }
		public ITransaction Transaction { get; private set; }
		public IContainer Container { get; private set; }

		public virtual void Setup()
		{
			Session = MockRepository.GenerateStub<ISession>();
			Transaction = MockRepository.GenerateStub<ITransaction>();
			Container = MockRepository.GenerateStub<IContainer>();

			Session.Stub(session => session.BeginTransaction()).Return(Transaction);

			PersistenceScope = new PersistenceScope(Session, Container);
		}
	}
}