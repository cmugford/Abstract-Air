using System;

using NHibernate;

using Rhino.Mocks;

namespace AbstractAir.Persistence.Tests
{
	public class PersistenceMessageModuleTestFixtureBase
	{
		public ISessionContextStrategy SessionContextStrategy { get; private set; }
		public ISessionFactory SessionFactory { get; private set; }
		public PersistenceMessageModule PersistenceMessageModule { get; private set; }

		public virtual void Setup()
		{
			SessionContextStrategy = MockRepository.GenerateStub<ISessionContextStrategy>();
			SessionFactory = MockRepository.GenerateStub<ISessionFactory>();

			PersistenceMessageModule = new PersistenceMessageModule(SessionContextStrategy, SessionFactory);
		}
	}
}