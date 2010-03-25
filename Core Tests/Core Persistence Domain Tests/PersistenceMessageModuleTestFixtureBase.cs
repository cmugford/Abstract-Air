using System;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceMessageModuleTestFixtureBase
	{
		public ISessionContextStrategy SessionContextStrategy { get; private set; }
		public ISessionFactory SessionFactory { get; private set; }
		public ISession Session { get; private set; }
		public PersistenceMessageModule PersistenceMessageModule { get; private set; }

		public virtual void Setup()
		{
			SessionContextStrategy = MockRepository.GenerateStub<ISessionContextStrategy>();
			SessionFactory = MockRepository.GenerateStub<ISessionFactory>();
			Session = MockRepository.GenerateStub<ISession>();

			SessionFactory.Stub(factory => factory.OpenSession()).Return(Session);
			SessionContextStrategy.Stub(strategy => strategy.Retrieve()).Return(Session);

			PersistenceMessageModule = new PersistenceMessageModule(SessionContextStrategy, SessionFactory);
		}
	}
}